using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class player1_script : MonoBehaviour
{
    private StatusScript status;
    private AreaSkillScript AreaSkill;
    public Animator anime;

    //　タッチした座標
    public Vector2 ScreenPos;
    //　ドラッグでまとめて選択されたキャラ
    SelectUnit selectunit;
    bool OnScreen = false;

    public NavMeshAgent agent;

    private RaycastHit hit;
    private Ray ray;

    public int HP;    //体力
   
    public int PlayerATK = 1;  //プレイヤーの攻撃力
    public int Number;   //キャラクターの記されている数字
    public bool death;
    public float Deathtime;
    public bool FlagTeam;
    //public int TeamNumber;
    private float BaseSpeed;

    public bool tasi;

    public List<int> edge;
    public int ID;
    public int rivalID;

   public int i2;//相手側の数値

    public int i3;//相手側の数値保管
    public bool zeroflag;
    public static bool SuutiByougaflag=false;
    public int N;

    //  キャラクターの内枠
    public GameObject zone;

    void Start()
    {
        status = GameObject.Find("Status").GetComponent<StatusScript>();
        AreaSkill = GameObject.Find("Area").GetComponent<AreaSkillScript>();
        anime = GetComponent<Animator>();
        //　まとめて選択したキャラを取得
        selectunit = GameObject.Find("_SelectUnitManager").GetComponent<SelectUnit>();

        agent = GetComponent<NavMeshAgent>();
        //　キャラ速度の設定
        //　基礎速度からキャラの数字割る２で0.5単位で変化させてる(＋Speedで外部から微調整できる(キャラ全体での調整、キャラ数値で調整したいなら計算式を変更する))
        BaseSpeed = status.CharBaseSpeed - Number / 2 + status.CharMainteSpeed;
        agent.speed = BaseSpeed;

        //    transform.rotation = Quaternion.Euler(0, 180, 0);
        HP = status.CharaHP; //初期体力を最大値にする
        death = false;
        tasi = true;
        zeroflag = false;
        FlagTeam = false;

        edge = new List<int>();

        //  初期は半透明
        zone.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 0.3f);



    }

    void Update()
    {
        SuutiScript.Number = Number;
        SuutiScript.i2 = i2;


        if (HP <= 0)
        {
            anime.SetBool("run", false);
            death = true;
        }

        //　椿井がエリアのマネージャーを作り次第デバッグする(終わるとこの一文削除)
        //if(/*スピードアップのエリアを取ってる*/)
        //{
            agent.speed = AreaSkill.MoveSpeedUp(BaseSpeed,status.AreaNum);
        //}
        //else //　スピードアップのエリアを取っていない場合 
        //{
        //    agent.speed = BaseSpeed;
        //}

        //　タッチした座標の取得
        ScreenPos = Camera.main.WorldToScreenPoint(this.transform.position);
        //　もしドラッグ選択した時にその範囲内に自分のキャラがいれば
        if (selectunit.UnitWithinScreenSpace(ScreenPos))
        {
            OnScreen = true;
            //　もし自分がドラッグ範囲内にいなかったら
            if (!selectunit.UnitsOnScreenSpace.Contains(this.gameObject))
                //　自分をリストに追加
                selectunit.UnitsOnScreenSpace.Add(this.gameObject);
        }
        else
        {
            if (OnScreen)
            {
                //　移動が終わったら自分をリストから削除
                selectunit.UnitsOnScreenSpace.Remove(this.gameObject);
                OnScreen = false;
            }
        }
        //　もしドラッグで選択されたキャラがいれば
        if (selectunit.selectedunit == this.gameObject || selectunit.selectedunits.Contains(this.gameObject))
        {
            //　右クリック
            if (Input.GetMouseButtonDown(1))
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                {
                    if (hit.transform.tag == "Floor")
                    {
                        //　キャラの移動、アニメーション、自分をリストから削除
                        agent.destination = hit.point;
                        anime.SetBool("run", true);
                        selectunit.selectedunits.Remove(this.gameObject);
                    }
                }
            }
        }
    }



    public void Pointer_Click()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100f))
        {
            agent.SetDestination(hit.point);
            anime.SetBool("run", true);
        }
    }
    

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player1")
        {
            SuutiByougaflag = false;
         if (tasi == true && zeroflag == false) i2 = Number;
        }

        if (Number >= i2)
        {
            if (tasi == true)
            {
                tasi = false;
                zeroflag = true;
            }
        }
        else if (Number < i2)
        {
            if (tasi == true)
            {
                tasi = false;
                zeroflag = true;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player1")
        {
            zeroflag = false;
            SuutiByougaflag = true;
        }

        if (Number > i2)
        {
            if (tasi == false)
            {
                tasi = true;
            }
        }

        else if (Number <= i2)
        {
            if (tasi == false)
            {
                tasi = true;
            }
        }
    }
}

