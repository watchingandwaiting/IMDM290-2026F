// UMD IMDM290 
// Instructor: Myungin Lee
// This tutorial introduce a way to draw spheres and align them in a circle with colors.

using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CreateShape : MonoBehaviour
{
    GameObject[] spheres;
    Vector3[] spheresInitialPositions;
    public int numSphere = 10;
    public int size = 10;
    public Texture2D shape;
    List<int> pixelsX = new List<int>();
    List<int> pixelsY = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        traceShape();
        spheres = new GameObject[numSphere]; // how many spheres
        spheresInitialPositions = new Vector3[numSphere]; // initial positions of the spheres

        float xScale = 1; //shape.Size().x / size;
        float yScale = 1; //shape.Size().y / size;
        int interval = pixelsX.Count / (numSphere - 1);
        int sphereCount = 0;

        for (int i = 0; i < pixelsX.Count; i++)
        {
            //Place a sphere every interval
            if(i % interval == 0)
            {
                Debug.Log(i + " % " + interval);
                //make a sphere
                spheres[sphereCount] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                spheresInitialPositions[sphereCount] = new Vector3((pixelsX[i] - pixelsX.Count/2) * xScale, (pixelsY[i] - pixelsY.Count/2) * yScale);

                spheres[sphereCount].transform.position = spheresInitialPositions[sphereCount];

                // Get the renderer of the spheres and assign colors.
                Renderer sphereRenderer = spheres[sphereCount].GetComponent<Renderer>();
                // hsv color space: https://en.wikipedia.org/wiki/HSL_and_HSV
                float hue = (float)sphereCount / numSphere; // Hue cycles through 0 to 1
                Color color = Color.HSVToRGB(hue, 1f, 1f); // Full saturation and brightness
                sphereRenderer.material.color = color;
                sphereCount++;
                //Debug.Log("Sphere " + i);
            }
        }
    }

    void traceShape()
    {
        //check every pixel
        for(int i=0; i < shape.Size().x; i++)
        {
            for(int j = 0; j < shape.Size().y; j++)
            {
                //if the pixel is transparent
                if(shape.GetPixel(i,j).a == 0)
                {
                    if(i < shape.Size().x && shape.GetPixel(i+1,j).a > 0)
                    {
                        pixelsX.Add(i);
                        pixelsY.Add(j);
                    }
                    if(j < shape.Size().y && shape.GetPixel(i,j+1).a > 0)
                    {
                        pixelsY.Add(i);
                        pixelsY.Add(j);
                    }
                }
            }
        }
    }
}
