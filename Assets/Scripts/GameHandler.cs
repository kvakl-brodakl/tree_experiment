using UnityEngine;
using System.Collections.Generic;

public class GameHandler : MonoBehaviour {
    
    public Material material;
    private int half = 1;
    private List<Vector3> _lverts;
    private List<int> _ltrls;

    private void Start() {
        _lverts = new List<Vector3>();
        _ltrls = new List<int>();
        
        int[][] cords = {
            new int[] {0, 1, 1},
            new int[] {1, 1, 0},
            new int[] {0, 1, -1},
            new int[] {-1, 1, 0},
            new int[] {2, 2, 0}

        };

        string[] axises = {
            "z",
            "x",
            "z",
            "x",
            "y"
        };
        
        for (int i = 0; i < axises.Length; i++) {
            AddVerts(cords[i], axises[i]);
        }

        Mesh mesh = new Mesh();

        mesh.vertices = _lverts.ToArray();
        mesh.triangles = _ltrls.ToArray();
        
        GameObject gameObject = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer));
        gameObject.transform.localPosition = new Vector3(0, 0, 0);
        // gameObject.transform.localScale = new Vector3(1, 1, 1);

        gameObject.GetComponent<MeshFilter>().mesh = mesh;

        gameObject.GetComponent<MeshRenderer>().material = material;
        // gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.TwoSided;
    }

    private void AddVerts(int[] cords, string axis) {

        Vector3[][] trls = GPlane(cords, axis);

        foreach (Vector3[] trl in trls) {
            Vector3 a = trl[0],
                    b = trl[1],
                    c = trl[2];

            if (_lverts.IndexOf(a) == -1) _lverts.Add(a);
            if (_lverts.IndexOf(b) == -1) _lverts.Add(b);
            if (_lverts.IndexOf(c) == -1) _lverts.Add(c);

            _ltrls.Add(_lverts.IndexOf(a));
            _ltrls.Add(_lverts.IndexOf(b));
            _ltrls.Add(_lverts.IndexOf(c));
        }

        // return(lverts, ltrls);
    }

    private Vector3[][] GPlane(int[] cords, string o) {
        int x = cords[0],
            y = cords[1],
            z = cords[2];

        Vector3[] vertices = new Vector3[4];
        Vector3[][] triangles = new Vector3[2][];
        triangles[0] = new Vector3[3];
        triangles[1] = new Vector3[3];
        
        
        switch(o) {
            case "x":
                vertices[0] = new Vector3(x, y + half, z + half);
                vertices[1] = new Vector3(x, y - half, z + half);
                vertices[2] = new Vector3(x, y + half, z - half);
                vertices[3] = new Vector3(x, y - half, z - half);
                break;
            case "y":
                vertices[0] = new Vector3(x + half, y, z + half);
                vertices[1] = new Vector3(x - half, y, z + half);
                vertices[2] = new Vector3(x + half, y, z - half);
                vertices[3] = new Vector3(x - half, y, z - half);
                break;
            case "z":
                vertices[0] = new Vector3(x + half, y + half, z);
                vertices[1] = new Vector3(x + half, y - half, z);
                vertices[2] = new Vector3(x - half, y + half, z);
                vertices[3] = new Vector3(x - half, y - half, z);
                break;
        }

        triangles[0][0] = vertices[0];
        triangles[0][1] = vertices[1];
        triangles[0][2] = vertices[2];
        triangles[1][0] = vertices[2];
        triangles[1][1] = vertices[1];
        triangles[1][2] = vertices[3];

        return triangles;
    }



}
