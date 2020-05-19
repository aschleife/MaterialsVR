using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class Isosurface_dev : MonoBehaviour
{
    Vector3Int dimensions;

    float[,,] data;
    List<List<Vector3>> atoms_position;
    List<int> atom_count;
    List<string> atom_name;

    public string file_name = "CHGCAR";
    public float atom_size = 1.0f;
    public bool draw_grid = true;
    public bool draw_atom = true;
    Vector3 scale;
    string data_path;

    //public float surface;
    float surface = 9.57048371525f; // CHGCAR
    //float surface = 429.8f; // CdS
    //float surface = 349.928951293f; //HgS

    float surface_change = 0.0f;

    public float noise = 20.0f;

    Mesh localMesh;

    MeshFilter meshFilter;

    // Start is called before the first frame update
    void Start()
    {
        scale = transform.localScale;
        data_path = "Assets/Isosurface/CHGCAR/" + file_name + ".vasp";
        localMesh = new Mesh();
        meshFilter = GetComponent<MeshFilter>();
        ReadData();
        GenerateMesh();
    }

    // Update is called once per frame
    void Update()
    {

        bool updateMesh = false;
        float speed = 0.10f;
        Vector3 scalar = new Vector3(0.01f, 0.01f, 0.01f);
        float surface_delta = 0.02f;
        //surface += Input.GetAxis("AXIS_4") * speed;
        this.transform.position -= Camera.main.transform.right * Input.GetAxis("AXIS_4") * speed;
        this.transform.position += Camera.main.transform.forward * Input.GetAxis("AXIS_5") * speed;
        surface_change += Input.GetAxis("AXIS_2") * surface_delta;
        //this.transform.Rotate(Vector3.down, Time.deltaTime * 10.0f);
        //if ((Input.GetAxis("AXIS_2") == 0) && (surface_change != 0) )
        //{
        //    surface += surface_change;
        //    surface_change = 0;
        //    GenerateMesh();
        //}
        if (Input.GetKey(KeyCode.O))
        {
            surface += surface * surface_delta;
        }
        if (Input.GetKey(KeyCode.I))
        {
            surface -= surface * surface_delta;
        }
        if (Input.GetKeyUp(KeyCode.O) || Input.GetKeyUp(KeyCode.I))
        {
            GenerateMesh();
        }
        
        
    }

    void DrawGrid()
    {
        Color line_color = Color.black;
        Vector3 point_0 = Vector3.zero;
        Vector3 point_1 = new Vector3(0.0f, dimensions.y, 0.0f);
        Vector3 point_2 = new Vector3(dimensions.x, dimensions.y, 0.0f);
        Vector3 point_3 = new Vector3(dimensions.x, 0.0f, 0.0f);
        Vector3 point_4 = new Vector3(0.0f, 0.0f, dimensions.z);
        Vector3 point_5 = new Vector3(0.0f, dimensions.y, dimensions.z);
        Vector3 point_6 = new Vector3(dimensions.x, dimensions.y, dimensions.z);
        Vector3 point_7 = new Vector3(dimensions.x, 0.0f, dimensions.z);
        DrawLine(point_0, point_1, scale, line_color);
        DrawLine(point_1, point_2, scale, line_color);
        DrawLine(point_2, point_3, scale, line_color);
        DrawLine(point_3, point_0, scale, line_color);
        DrawLine(point_4, point_5, scale, line_color);
        DrawLine(point_5, point_6, scale, line_color);
        DrawLine(point_6, point_7, scale, line_color);
        DrawLine(point_7, point_4, scale, line_color);
        DrawLine(point_0, point_4, scale, line_color);
        DrawLine(point_1, point_5, scale, line_color);
        DrawLine(point_2, point_6, scale, line_color);
        DrawLine(point_3, point_7, scale, line_color);
    }

    void UpdateBoxCollider()
    {
        BoxCollider box = GetComponent<BoxCollider>();
        Vector3 dim = dimensions;
        box.center = dim / 2f;
        box.size = dim;

    }

    void DrawAtoms()
    {
        foreach (List<Vector3> atom_list in atoms_position)
        {
            Color draw_color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
            foreach (Vector3 atom in atom_list)
            {
                DrawSphere(Vector3.Scale(Vector3.Scale(atom, dimensions), scale), draw_color);
            }
        }
    }

    void DrawSphere(Vector3 position, Color color)
    {
        GameObject mySphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        mySphere.transform.position = position;
        mySphere.GetComponent<MeshRenderer>().material.color = color;
    }

    void DrawLine(Vector3 start, Vector3 end, Vector3 scale, Color color)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Standard"));
        lr.startColor = color;
        lr.endColor = color;
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.SetPosition(0, Vector3.Scale(start, scale));
        lr.SetPosition(1, Vector3.Scale(end, scale));
    }

    void ReadData()
    {
        using (StreamReader reader = new StreamReader(data_path))
        {
            string line;
            string[] entries;
            char[] separator = new char[] {' '};

            int counter = 0;
            int counter2 = 0;
            float data_max = 0.0f;
            float data_sum = 0.0f;

            // Skip to isosurface data
            for (int i = 0; i < 5; i++)
            {
                line = reader.ReadLine();
            }
            line = reader.ReadLine();
            entries = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            atom_name = new List<string>();
            foreach (string entry in entries)
            {
                atom_name.Add(entry);
            }
            line = reader.ReadLine();
            entries = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            atom_count = new List<int>();
            int line_count = 0;
            foreach (string entry in entries)
            {
                line_count += Convert.ToInt32(entry);
                atom_count.Add(Convert.ToInt32(entry));
            }
            line = reader.ReadLine();

            atoms_position = new List<List<Vector3>>();
            foreach (int atom_num in atom_count)
            {
                List<Vector3> atom_list = new List<Vector3>();
                for (int i = 0; i < atom_num; i++)
                {
                    line = reader.ReadLine();
                    entries = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    Vector3 atom_pos = new Vector3(Convert.ToSingle(entries[0]), Convert.ToSingle(entries[1]), Convert.ToSingle(entries[2]));
                    atom_list.Add(atom_pos);
                }
                atoms_position.Add(atom_list);
            }
            line = reader.ReadLine();

            // Read dimensions
            line = reader.ReadLine();
            
            entries = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            dimensions.x = Convert.ToInt32(entries[0]);
            dimensions.y = Convert.ToInt32(entries[1]);
            dimensions.z = Convert.ToInt32(entries[2]);
            float delta = 0.1f;
            float sphere_scale = 0.1f;
            Debug.Log(dimensions);
            data = new float[dimensions.x, dimensions.y, dimensions.z];
            int ent_per_line = 10;
            
            // Read data
            for (int z = 0; z < dimensions.z; z++)
            {
                for (int y = 0; y < dimensions.y; y++)
                {
                    for (int x = 0; x < dimensions.x; x++)
                    {
                        
                        int idx = x + y * dimensions.x + z * dimensions.x * dimensions.y;
                        if (idx == 0)
                        {
                             line = reader.ReadLine();
                             entries = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                             ent_per_line = entries.Length;
                        }
                        else if (idx % ent_per_line == 0)
                        {
                            line = reader.ReadLine();
                            entries = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                        }
                        data[x,y,z] = Convert.ToSingle(entries[idx % ent_per_line]);
                        counter++;
                        data_sum += data[x, y, z];
                        if (data[x,y,z] > data_max)
                        {
                            data_max = data[x, y, z];
                        }
                        if (Math.Abs(data[x, y, z] - surface) < delta)
                        {
                        //    var mySphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        //    mySphere.transform.position = new Vector3(x, y, z);
                        //    mySphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                            counter2++;
                        }
                    }
                }
            }
            // Debug.Log(counter);
            // Debug.Log(counter2);
            Debug.Log(data_max);
            Debug.Log(data_sum);
            // Set surface to average
            //surface = data_sum / dimensions.x / dimensions.y / dimensions.z;
        }
    }

    void GenerateMesh()
    {
        int vertexIndex = 0;
        Vector3[] interpolatedValues = new Vector3[12];

        List<Vector3> vertices = new List<Vector3>();
        List<int> triangleIndices = new List<int>();

        for (int x = 0; x < dimensions.x - 1; x++)
        {
            for (int y = 0; y < dimensions.y - 1; y++)
            {
                for (int z = 0; z < dimensions.z - 1; z++)
                {
                    //if (vertices.Count > 64000)
                    //{
                    //    Debug.Log("Vertice Limit Reached");
                    //    break;
                    //}

                    Vector3 basePoint = new Vector3(x, y, z);

                    //Get the 8 corners of this cube
                    float p0 = data[x, y, z];
                    float p1 = data[x + 1, y, z];
                    float p2 = data[x, y + 1, z];
                    float p3 = data[x + 1, y + 1, z];
                    float p4 = data[x, y, z + 1];
                    float p5 = data[x + 1, y, z + 1];
                    float p6 = data[x, y + 1, z + 1];
                    float p7 = data[x + 1, y + 1, z + 1];

                    //A bitmap indicating which edges the surface of the volume crosses
                    int crossBitMap = 0;


                    if (p0 < surface)
                        crossBitMap |= 1;
                    if (p1 < surface)
                        crossBitMap |= 2;

                    if (p2 < surface)
                        crossBitMap |= 8;
                    if (p3 < surface)
                        crossBitMap |= 4;

                    if (p4 < surface)
                        crossBitMap |= 16;
                    if (p5 < surface)
                        crossBitMap |= 32;

                    if (p6 < surface)
                        crossBitMap |= 128;
                    if (p7 < surface)
                        crossBitMap |= 64;

                    //Use the edge look up table to determin the configuration of edges
                    int edgeBits = Contouring3D.EdgeTableLookup[crossBitMap];

                    //The surface did not cross any edges, this cube is either complelety inside, or completely outside the volume
                    if (edgeBits == 0)
                        continue;

                    float interpolatedCrossingPoint = 0.0f;

                    //Calculate the interpolated positions for each edge that has a crossing value

                    //Bottom four edges
                    if ((edgeBits & 1) > 0)
                    {
                        interpolatedCrossingPoint = (surface - p0) / (p1 - p0);
                        interpolatedValues[0] = Vector3.Lerp(new Vector3(x, y, z), new Vector3(x + 1, y, z), interpolatedCrossingPoint);
                    }

                    if ((edgeBits & 2) > 0)
                    {
                        interpolatedCrossingPoint = (surface - p1) / (p3 - p1);
                        interpolatedValues[1] = Vector3.Lerp(new Vector3(x + 1, y, z), new Vector3(x + 1, y + 1, z), interpolatedCrossingPoint);
                    }

                    if ((edgeBits & 4) > 0)
                    {
                        interpolatedCrossingPoint = (surface - p2) / (p3 - p2);
                        interpolatedValues[2] = Vector3.Lerp(new Vector3(x, y + 1, z), new Vector3(x + 1, y + 1, z), interpolatedCrossingPoint);
                    }

                    if ((edgeBits & 8) > 0)
                    {
                        interpolatedCrossingPoint = (surface - p0) / (p2 - p0);
                        interpolatedValues[3] = Vector3.Lerp(new Vector3(x, y, z), new Vector3(x, y + 1, z), interpolatedCrossingPoint);
                    }

                    //Top four edges
                    if ((edgeBits & 16) > 0)
                    {
                        interpolatedCrossingPoint = (surface - p4) / (p5 - p4);
                        interpolatedValues[4] = Vector3.Lerp(new Vector3(x, y, z + 1), new Vector3(x + 1, y, z + 1), interpolatedCrossingPoint);
                    }

                    if ((edgeBits & 32) > 0)
                    {
                        interpolatedCrossingPoint = (surface - p5) / (p7 - p5);
                        interpolatedValues[5] = Vector3.Lerp(new Vector3(x + 1, y, z + 1), new Vector3(x + 1, y + 1, z + 1), interpolatedCrossingPoint);
                    }

                    if ((edgeBits & 64) > 0)
                    {
                        interpolatedCrossingPoint = (surface - p6) / (p7 - p6);
                        interpolatedValues[6] = Vector3.Lerp(new Vector3(x, y + 1, z + 1), new Vector3(x + 1, y + 1, z + 1), interpolatedCrossingPoint);
                    }

                    if ((edgeBits & 128) > 0)
                    {
                        interpolatedCrossingPoint = (surface - p4) / (p6 - p4);
                        interpolatedValues[7] = Vector3.Lerp(new Vector3(x, y, z + 1), new Vector3(x, y + 1, z + 1), interpolatedCrossingPoint);
                    }

                    //Side four edges
                    if ((edgeBits & 256) > 0)
                    {
                        interpolatedCrossingPoint = (surface - p0) / (p4 - p0);
                        interpolatedValues[8] = Vector3.Lerp(new Vector3(x, y, z), new Vector3(x, y, z + 1), interpolatedCrossingPoint);
                    }

                    if ((edgeBits & 512) > 0)
                    {
                        interpolatedCrossingPoint = (surface - p1) / (p5 - p1);
                        interpolatedValues[9] = Vector3.Lerp(new Vector3(x + 1, y, z), new Vector3(x + 1, y, z + 1), interpolatedCrossingPoint);
                    }

                    if ((edgeBits & 1024) > 0)
                    {
                        interpolatedCrossingPoint = (surface - p3) / (p7 - p3);
                        interpolatedValues[10] = Vector3.Lerp(new Vector3(x + 1, y + 1, z), new Vector3(x + 1, y + 1, z + 1), interpolatedCrossingPoint);
                    }

                    if ((edgeBits & 2048) > 0)
                    {
                        interpolatedCrossingPoint = (surface - p2) / (p6 - p2);
                        interpolatedValues[11] = Vector3.Lerp(new Vector3(x, y + 1, z), new Vector3(x, y + 1, z + 1), interpolatedCrossingPoint);
                    }

                    crossBitMap <<= 4;

                    int triangleIndex = 0;
                    while (Contouring3D.TriangleLookupTable[crossBitMap + triangleIndex] != -1)
                    {
                        int index1 = Contouring3D.TriangleLookupTable[crossBitMap + triangleIndex];
                        int index2 = Contouring3D.TriangleLookupTable[crossBitMap + triangleIndex + 1];
                        int index3 = Contouring3D.TriangleLookupTable[crossBitMap + triangleIndex + 2];

                        vertices.Add(new Vector3(interpolatedValues[index1].x, interpolatedValues[index1].y, interpolatedValues[index1].z));
                        vertices.Add(new Vector3(interpolatedValues[index2].x, interpolatedValues[index2].y, interpolatedValues[index2].z));
                        vertices.Add(new Vector3(interpolatedValues[index3].x, interpolatedValues[index3].y, interpolatedValues[index3].z));

                        triangleIndices.Add(vertexIndex);
                        triangleIndices.Add(vertexIndex + 1);
                        triangleIndices.Add(vertexIndex + 2);
                        //triangleIndices.Add(vertexIndex);
                        //triangleIndices.Add(vertexIndex + 2);
                        //triangleIndices.Add(vertexIndex + 1);
                        vertexIndex += 3;
                        triangleIndex += 3;
                    }
                }
            }
        }

        List<Vector2> texCoords = new List<Vector2>();
        Vector2 emptyTexCoords0 = new Vector2(0, 0);
        Vector2 emptyTexCoords1 = new Vector2(0, 1);
        Vector2 emptyTexCoords2 = new Vector2(1, 1);

        for (int texturePointer = 0; texturePointer < vertices.Count; texturePointer += 3)
        {
            texCoords.Add(emptyTexCoords1);
            texCoords.Add(emptyTexCoords2);
            texCoords.Add(emptyTexCoords0);
        }

        //Generate the mesh using the vertices and triangle indices we just created
        if (localMesh != null)
        {
            localMesh.Clear();
            localMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

            //Vector3[] vert = vertices.ToArray();
            //int[] trig = triangleIndices.ToArray();
            //Vector3[] vertDiscrete = new Vector3[trig.Length];
            //int[] trigDiscrete = new int[trig.Length];
            //for (int i = 0; i < trig.Length; i++)
            //{
            //    vertDiscrete[i] = vert[trig[i]];
            //    trigDiscrete[i] = i;
            //}
            //localMesh.vertices = vertDiscrete;
            //localMesh.triangles = trigDiscrete;

            localMesh.vertices = vertices.ToArray();
            localMesh.triangles = triangleIndices.ToArray();

            // Calculate normals
            // answers.unity.com/questions/630505/how-to-procedurally-generate-smooth-meshes.html
            //Vector3[] normals = new Vector3[vertices.Count];
            //List<Vector3>[] vertexNormals = new List<Vector3>[vertices.Count];
            //for (int i = 0; i < vertexNormals.Length; i++)
            //{
            //    vertexNormals[i] = new List<Vector3>();
            //}
            //for (int i = 0; i < triangleIndices.Count; i += 3)
            //{
            //    Vector3 currNormal = Vector3.Cross(
            //        (vertices[triangleIndices[i + 1]] - vertices[triangleIndices[i]]).normalized,
            //        (vertices[triangleIndices[i + 2]] - vertices[triangleIndices[i]]).normalized);

            //    vertexNormals[triangleIndices[i]].Add(currNormal);
            //    vertexNormals[triangleIndices[i + 1]].Add(currNormal);
            //    vertexNormals[triangleIndices[i + 2]].Add(currNormal);
            //}
            //for (int i = 0; i < vertexNormals.Length; i++)
            //{
            //    normals[i] = Vector3.zero;
            //    float numNormals = vertexNormals[i].Count;
            //    for (int j = 0; j < numNormals; j++)
            //    {
            //        normals[i] += vertexNormals[i][j];
            //    }
            //    normals[i] /= numNormals;
            //}
            //localMesh.normals = normals;


            localMesh.uv = texCoords.ToArray();
            localMesh.RecalculateNormals();
            localMesh.RecalculateBounds();
        }
        meshFilter.mesh = localMesh;

        if (draw_grid) { }
            DrawGrid();
        if (draw_atom)
            DrawAtoms();
        UpdateBoxCollider();
    }

}
