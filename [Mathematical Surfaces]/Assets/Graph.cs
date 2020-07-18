using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEditor;
using UnityEngine;

public class Graph : MonoBehaviour {
    public Transform pointPrefab;

    public Transform[] points;

    bool hasAwakend = false;

    [Range(10, 100)]
    public int resolution = 10;

    public GraphFunctionName function;

    private static GraphFunction[] functions = {
            SineFunction, MultiSineFunction
    };

    void Awake() {
        InitialiseCubes();
        hasAwakend = true;
    }

    void OnValidate() {
        if (EditorApplication.isPlaying) {
            if (hasAwakend) {
                for (int i = 0; i < points.Length; i++) {
                    Destroy(points[i].gameObject);
                }
                InitialiseCubes();
            }
        }
    }

    void Update() {
        float t = Time.time;
        GraphFunction f = functions[(int) function];

        for (int i = 0; i < points.Length; i++) {
            Transform point = points[i];
            Vector3 position = point.localPosition;
            position.y = f(position.x, t);
            point.localPosition = position;
        }
    }

    static float SineFunction(float x, float t) {
        return Mathf.Sin(Mathf.PI * (x + t));
    }

    static float MultiSineFunction(float x, float t) {
        float y = Mathf.Sin(Mathf.PI * (x + t));
        y += Mathf.Sin(2f * Mathf.PI * (x + t)) / 2f;
        return y;
    }

    private void InitialiseCubes() {
        float step = 2f / resolution;
        Vector3 scale = Vector3.one * step;
        Vector3 position = Vector3.zero;
        points = new Transform[resolution];
        for (int i = 0; i < points.Length; i++) {
            Transform point = Instantiate(pointPrefab);
            points[i] = point;
            position.x = (i + 0.5f) * step - 1f;

            point.localPosition = position;
            point.localScale = scale;
            point.SetParent(transform, false);
        }
    }


}
