using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveEnemy : MonoBehaviour {

	GameObject player;

	private Rigidbody2D rb2d;
	Rigidbody2D rigidbody2D;
	public int speed = -3;
	public float waitTime = 1.0f ;
	bool waiting = false ;
	Vector3 initScale ;
	Vector3 revScale ;
	bool randomMove = true ;
	public float chaseDistance = 2.0f;
	public float randTimeMin = 1.0f ;
	public float randTimeMax = 8.0f ;
	int randDir = 0;
	public float chase = 3.0f;//敵との範囲
	public float moveSpeed = 1.0f;//敵のスピード
    public AudioClip EnemySound;



	void Start () {
		// オブジェクトのRigidbody2Dを取得
		rb2d = GetComponent<Rigidbody2D> ();
		// PLAYERオブジェクトを取得
		player = GameObject.Find ("player");

		initScale = transform.localScale;
		revScale = new Vector3 (initScale.x * -1.0f, initScale.y, initScale.z);

		StartCoroutine ("setRundomTimeAndDirection");

	}



	void Update () {
        if (GlobalParameters.kakureta == false)
        {
            oikakeru();
        }

	}

    void oikakeru(){
        Vector3 diff = player.transform.position - gameObject.transform.position;


        if (diff.magnitude < chase && GlobalParameters.black == false )
        {
            Vector2 v;
            v.x = diff.x;
            v.y = diff.y;
            v.Normalize();//敵の走る早さを一定に

            rb2d.velocity = v * moveSpeed;

            //AudioSource EnemyAudiosource = gameObject.GetComponents<AudioSource>()[0];
            //EnemyAudiosource.Play();
            //EnemyAudiosource.Stop();



            diff.Normalize();
            //Debug.Log(Vector2.Angle(diff, Vector2.down));
            if (Vector2.Angle(diff, Vector2.up) < 45.0f)
            {
                GetComponent<Animator>().SetInteger("Direction", 2);//プレイヤーに向けるベクトルの調整(向きの調整)
                Debug.Log("up");

            }
            else if (Vector2.Angle(diff, Vector2.down) < 45.0f)
            {
                GetComponent<Animator>().SetInteger("Direction", 1);
                Debug.Log("down");

            }
            else if (Vector2.Angle(diff, Vector2.right) < 45.0f)
            {
                GetComponent<Animator>().SetInteger("Direction", 3);
                Debug.Log("right");
            }
            else if (Vector2.Angle(diff, Vector2.left) < 45.0f)
            {
                GetComponent<Animator>().SetInteger("Direction", 4);
                Debug.Log("left");
            }
        }

        else
        {
            if (randomMove)
            {
                // ランダムに動かす(範囲の中に入ってない場合)

                AudioSource EnemyAudiosource = gameObject.GetComponents<AudioSource>()[0];
                EnemyAudiosource.Play();





                float distanceFromPlayer = Vector3.Distance(player.transform.position, gameObject.transform.position);
                // Debug.Log (distanceFromPlayer);
                if (randDir == 4)
                {
                    rb2d.velocity = Vector2.left * moveSpeed;
                    transform.localScale = initScale;
                    GetComponent<Animator>().SetInteger("Direction", 4);//アニメーション切り替え

                }
                else if (randDir == 3)
                {
                    rb2d.velocity = Vector2.right * moveSpeed;
                    transform.localScale = revScale;
                    GetComponent<Animator>().SetInteger("Direction", 3);



                }
                else if (randDir == 2)
                {
                    rb2d.velocity = Vector2.up * moveSpeed;
                    // transform.localScale = revScale;
                    // Animationの切り替えをする、
                    GetComponent<Animator>().SetInteger("Direction", 2);



                }
                else if (randDir == 1)
                {
                    rb2d.velocity = Vector2.down * moveSpeed;
                    // transform.localScale = revScale;
                    // Animationの切り替えをする、
                    GetComponent<Animator>().SetInteger("Direction", 1);

                }

                if (distanceFromPlayer < chaseDistance)
                {
                    randomMove = false;
                }
            }
            else
            {
                // 移動関数の呼び出し
                if (waiting == false)
                {
                    EnemyMove();
                }
            }
        }
        
    }

	// ENEMYの移動関数1フレーム毎にUpdate関数から呼び出される
	void EnemyMove () {
		if (player.GetComponent<Player> ().isGameOver) {
			rb2d.velocity = Vector2.zero;
			return;

			// PLAYERの位置を取得
			Vector2 targetPos = player.transform.position;
			// PLAYERのx座標
			float x = targetPos.x;
			// ENEMYは、地面を移動させるので座標は常に0とする
			float y = 0;
			// 移動を計算させるための２次元のベクトルを作る
			Vector2 direction = new Vector2 (
				                   x - transform.position.x, y).normalized;
			// ENEMYのRigidbody2Dに移動速度を指定する
			rb2d.velocity = direction * 6;


			if (direction.x < 0) {
				transform.localScale = initScale;
			} else {
				transform.localScale = revScale;
			}

		}    
		//private void OnCollisionEnter2D(Collision2D collision)
		//{
		//if ( collision.gameObject.tag == "Player")

		//}


	}

	private IEnumerator setRundomTimeAndDirection() {
		randDir = Random.Range (0,4);

		float radTime = Random.Range (randTimeMin, randTimeMax);
		yield return new WaitForSeconds (radTime);

		StartCoroutine ("setRundomTimeAndDirection");
	}  

}


