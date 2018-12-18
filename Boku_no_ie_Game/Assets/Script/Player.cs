using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
	public bool hasKey = false ;  // 鍵をゲットしたフラグ
	public GameObject keyImage;
    public GameObject keyImage2;
    public GameObject keyImage3;
    public GameObject oshiraseImage;
	public bool isGameOver = false;
	public float speed;
	private Vector3 lastPosition;
	float lastMoveTime;
	public GameObject[] encountCharactors;
	public string nextScene;
	public GameObject black; // 真っ黒
	public GameObject spot; // スポットライト
	bool spotMode = true ; // スポットライトがonのフラグ

	public AudioClip getSound;//アイテムゲット音
	public AudioClip ExitSound;
    public AudioClip NoExitSound;
    public AudioClip EncountSound;
    public AudioClip WalkSound;
    public RuntimeAnimatorController batAnim;


	[System.NonSerialized]
	public bool talking = false;

	public GameObject speechObject ;
   

	string lastEncount = "";
	private List<string> items;


	// Use this for initialization
	void Start () {
		lastPosition = transform.position;
		items = new List<string>();
	}


	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

		if (talking)
		{
			return;
		}

	//↓歩く動き
	// Update is called once per frame
	
		if (Input.GetKey (KeyCode.DownArrow)) {
			gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * (-1.0f * speed );
			GetComponent<Animator> ().SetInteger ("Direction", 1);//←アニメーション切り替え
			lastMoveTime = Time.time;
            gameObject.GetComponent<AudioSource>().PlayOneShot(WalkSound);
		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
			GetComponent<Animator> ().SetInteger ("Direction", 2);
			lastMoveTime = Time.time;
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.right * speed;
			GetComponent<Animator> ().SetInteger ("Direction", 3);
			lastMoveTime = Time.time;
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.right * (-1.0f * speed );
			GetComponent<Animator> ().SetInteger ("Direction", 4);
			lastMoveTime = Time.time;
		}

		if (Input.GetKeyUp (KeyCode.LeftArrow)) {
			gameObject.GetComponent<Animator> ().enabled = false;
		}

		if (Input.GetKeyUp (KeyCode.RightArrow)) {
			gameObject.GetComponent<Animator> ().enabled = false;
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			gameObject.GetComponent<Animator> ().enabled = true;
		}

		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			gameObject.GetComponent<Animator> ().enabled = true;
		}

		if (Input.GetKeyUp (KeyCode.DownArrow)) {
			gameObject.GetComponent<Animator> ().enabled = false;
		}

		if (Input.GetKeyUp (KeyCode.UpArrow)) {
			gameObject.GetComponent<Animator> ().enabled = false;
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			gameObject.GetComponent<Animator> ().enabled = true;
		}

		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			gameObject.GetComponent<Animator> ().enabled = true;
		}

        if (Input.GetMouseButtonDown(0))
        {
            oshiraseImage.SetActive(false);
        }
		lastPosition = transform.position;

		// スペースキーで真っ黒 -> スポット
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (spotMode) {
				spotLightOff ();
			} else {
				spotLightOn ();
			}
		}

	}

	public void spotLightOn(){
		// スポットにする
		black.SetActive(false);
		spot.SetActive(true);
		spotMode = true;
	}

	public void spotLightOff(){
		// 黒にする
		black.SetActive(true);
		spot.SetActive(false);
		spotMode = false;
        GlobalParameters.black = true;
	}

	void dispEncount(GameObject triggerObjct)
	{
		GameObject go = triggerObjct.GetComponent<EncountStart>().EncountObject;
		if (go == null)
		{
			return;
		}


		if (lastEncount != go.name)
		{
			if (talking == false)
			{
				go.SetActive(true);
				speechObject.SetActive(true);
				speechObject.GetComponent<Speech>().startSpeech(go.GetComponent<Encounter>().sentences);
				talking = true;

			}
		}

		lastEncount = go.name;

	}
	void dispItem(GameObject triggerObjct)
	{
		GameObject go = triggerObjct.GetComponent<ItemStart>().ItemObject;
		if ( go == null)
		{
			return;
		}

		if ( go.activeSelf)
		{
			return;
		}
		go.SetActive(true);
		speechObject.SetActive(true);
		Debug.Log (speechObject.GetComponent<Speech> ());
		Debug.Log (go.GetComponent<Item>());
		speechObject.GetComponent<Speech>().startSpeech(go.GetComponent<Item>().sentences);
		talking = true;

		items.Add(go.name);

	}

    bool checkHasKey()
    {
        return items.Contains("kagi");
    }


	void OnCollisionEnter2D(Collision2D collider)
	{
		Debug.Log(collider.gameObject.tag);

        if (collider.gameObject.tag == "hasami")
        {
            // Keyをとったときの処理
            keyImage.SetActive(true);
            gameObject.GetComponent<AudioSource>().PlayOneShot(getSound);
            GlobalParameters.keyImage = true;

            if (GlobalParameters.keyImage == true){
                Destroy(collider.gameObject);
            }



		}

        if (collider.gameObject.tag == "bat")
        {
            // Keyをとったときの処理
            keyImage2.SetActive(true);
            Destroy(collider.gameObject);
            gameObject.GetComponent<AudioSource>().PlayOneShot(getSound);
            GlobalParameters.keyImage2 = true;


            gameObject.GetComponent<Animator>().runtimeAnimatorController = batAnim;

        }

        if (collider.gameObject.tag == "kagi")
        {
            // Keyをとったときの処理
            keyImage3.SetActive(true);
            Destroy(collider.gameObject);
            hasKey = true;
            gameObject.GetComponent<AudioSource>().PlayOneShot(getSound);
            GlobalParameters.keyImage3 = true;







        }

        /*
        if ( GlobalParameters.hasami ){
            // 入れる処理
        }else{
            // 入れません
        }
        */
       




		if (collider.gameObject.tag == "ItemTrigger")
		{
			dispItem(collider.gameObject);
		}

		if ( collider.gameObject.tag == "EncountTrigger")
	{
		dispEncount(collider.gameObject);
	}


		if (collider.gameObject.tag == "ExitTrigger")
		{  
			Debug.Log ("ExitTrigger");
            Debug.Log(collider.gameObject.name);



			if (collider.gameObject.name == "rouka_he") {
				PositionManager.setPos (SceneManager.GetActiveScene ().name, gameObject.transform.position);
				SceneNavigator.Instance.Change("rouka", 1.5f);
				//SceneManager.LoadScene("3gousya");
				gameObject.GetComponent<AudioSource> ().PlayOneShot (ExitSound);
			}

            if (collider.gameObject.name == "rouka2_he")
            {
                PositionManager.setPos(SceneManager.GetActiveScene().name, gameObject.transform.position);
                SceneNavigator.Instance.Change("rouka _hajime", 1.5f);
                //SceneManager.LoadScene("3gousya");
                gameObject.GetComponent<AudioSource>().PlayOneShot(ExitSound);
            }

            if (collider.gameObject.name == "kodomobeya_he")
            {
                PositionManager.setPos(SceneManager.GetActiveScene().name, gameObject.transform.position);
                SceneNavigator.Instance.Change("Boku_no_heya", 1.5f);
                //SceneManager.LoadScene("3gousya");
                gameObject.GetComponent<AudioSource>().PlayOneShot(ExitSound);
            }

            if (collider.gameObject.name == "bed_deguti")
            {
                PositionManager.setPos(SceneManager.GetActiveScene().name, gameObject.transform.position);
                SceneNavigator.Instance.Change("oya_sinnsitu", 0.2f);
                //SceneManager.LoadScene("3gousya");
                gameObject.GetComponent<AudioSource>().PlayOneShot(ExitSound);
            }

            if (collider.gameObject.name == "monooki_he")
            {

 

                if(GlobalParameters.keyImage3 == true)
              {
                    PositionManager.setPos(SceneManager.GetActiveScene().name, gameObject.transform.position);
                  SceneNavigator.Instance.Change("monooki", 1.5f);
                    //SceneManager.LoadScene("3gousya");
                   gameObject.GetComponent<AudioSource>().PlayOneShot(ExitSound);



             }
              else
          {
                   oshiraseImage.SetActive(true);
                   gameObject.GetComponent<AudioSource>().PlayOneShot(NoExitSound);
                   
                }
            }


            if (collider.gameObject.name == "kuro-zet_deguti")
            {
                PositionManager.setPos(SceneManager.GetActiveScene().name, gameObject.transform.position);
                SceneNavigator.Instance.Change("monooki", 0.2f);
                //SceneManager.LoadScene("3gousya");
                gameObject.GetComponent<AudioSource>().PlayOneShot(ExitSound);
            }

            if (collider.gameObject.name == "sinnsitu_he")
            {
                PositionManager.setPos(SceneManager.GetActiveScene().name, gameObject.transform.position);
                SceneNavigator.Instance.Change("oya_sinnsitu", 1.5f);
                //SceneManager.LoadScene("3gousya");
                gameObject.GetComponent<AudioSource>().PlayOneShot(ExitSound);
            }

            if (collider.gameObject.name == "rouka_he3")
            {
                PositionManager.setPos(SceneManager.GetActiveScene().name, gameObject.transform.position);
                SceneNavigator.Instance.Change("rouka", 1.5f);
                //SceneManager.LoadScene("3gousya");
                gameObject.GetComponent<AudioSource>().PlayOneShot(ExitSound);
            }

            if (collider.gameObject.name == "rouka4_he")
            {
                PositionManager.setPos(SceneManager.GetActiveScene().name, gameObject.transform.position);
                SceneNavigator.Instance.Change("rouka", 1.5f);
                //SceneManager.LoadScene("3gousya");
                gameObject.GetComponent<AudioSource>().PlayOneShot(ExitSound);
            }

            if (collider.gameObject.name == "toire_he")
            {
                PositionManager.setPos(SceneManager.GetActiveScene().name, gameObject.transform.position);
                SceneNavigator.Instance.Change("toire", 1.5f);
                //SceneManager.LoadScene("3gousya");
                gameObject.GetComponent<AudioSource>().PlayOneShot(ExitSound);
            }


			//if (checkHasKey())
			//{
			//    SceneManager.LoadScene(nextScene);
			//}
		}



	

}




}
