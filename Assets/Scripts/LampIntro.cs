using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampIntro : MonoBehaviour
{
	private int speed = 45;
	private int deep = 20;
	private float timeLeft = 2.5F;
	[SerializeField] private bool intro;
	[SerializeField] private Vector3 start;
    [SerializeField] private Vector3 end;
    [SerializeField] private Vector3 target;
	[SerializeField] GameObject backIntro;
	[SerializeField] GameObject main;

    void Start() {
        start = transform.position;
        end = transform.position;
        end.y += deep;
        target = end;
		intro = true;
    }

	void Update () {
		if (intro)
		{
			transform.position = Vector3.MoveTowards (transform.position, target, Time.deltaTime * speed);
			if (transform.position == end)
				target = start;
			else if (transform.position == start)
				target = end;
			
			timeLeft -= Time.deltaTime;
			if ( timeLeft < 0 )
			{
				intro = false;
				// gameObject.SetActive (false);
				// introText.SetActive (false);
				main.SetActive (true);
				backIntro.SetActive (false);
			}
		}
	}
}
