using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEditor;
using UnityEngine;

public class Graph : MonoBehaviour {
    const float pi = Mathf.PI;

    public Transform pointPrefab;

    public Transform[] points;

    bool hasAwakend = false;

    [Range(10, 100)]
    public int resolution = 10;

    public GraphFunctionName function;

    private static GraphFunction[] functions = {
            SineFunction, Sine2DFunction, MultiSineFunction, MultiSine2DFunction
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
            position.y = f(position.x, position.z, t);
            point.localPosition = position;
        }
    }

    static float SineFunction(float x, float z, float t) {
        return Mathf.Sin(pi * (x + t));
    }

    static float MultiSineFunction(float x, float z, float t) {
        float y = Mathf.Sin(pi * (x + t));
        y += Mathf.Sin(2f * pi * (x + t)) / 2f;
        return y;
    }
    static float Sine2DFunction(float x, float z, float t) {
        float y = Mathf.Sin(pi * (x + t));
        y += Mathf.Sin(pi * (z + t));
        y *= 0.5f;
        return y;
    }
    static float MultiSine2DFunction(float x, float z, float t) {
        float y = 4f * Mathf.Sin(pi * (x + z + t * 0.5f));
        y += Mathf.Sin(pi * (x + t));
        y += Mathf.Sin(2f * pi * (z + 2f * t)) * 0.5f;
        y *= 1f / 5.5f;
        return y;
    }

    private void InitialiseCubes() {
        float step = 2f / resolution;
        Vector3 scale = Vector3.one * step;
        Vector3 position = Vector3.zero;
        points = new Transform[resolution * resolution];
        for (int i = 0, z = 0; z < resolution; z++) {
            position.z = (z + 0.5f) * step - 1f;
            for (int x = 0; x < resolution; x++, i++) {
                Transform point = Instantiate(pointPrefab);
                points[i] = point;
                position.x = (x + 0.5f) * step - 1f;
                point.localPosition = position;
                point.localScale = scale;
                point.SetParent(transform, false);
            }
        }
    }


}
