using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampIntro : MonoBehaviour
{
	public float speed, deep;
	[SerializeField] private Vector3 start;
    [SerializeField] private Vector3 end;
    [SerializeField] private Vector3 target;

    void Start() {
        start = transform.position;
        end = transform.position;
        end.y += deep;
        target = end;
    }

	void Update () {
		transform.position = Vector3.MoveTowards (transform.position, target, Time.deltaTime * speed);
		if (transform.position == end)
			target = start;
		else if (transform.position == start)
			target = end;

		// transform.Rotate (Vector3.up * tilt);
	}
}
