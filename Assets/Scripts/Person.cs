using OpenCVForUnity;
using UnityEngine;
using Rect = OpenCVForUnity.Rect;

public class Person
{

    public enum RecognizeState : int
    {
        PENDING = 0,
        RECOGNIZED = 1,
        NOTRECOGNIZED = 2
    }

    public long? id;
    public string username = "";
    public string detail;
    public float? score = 1.0f;
    public Color color = Color.white;
    public byte[] face;
    public RecognizeState recognizeState = RecognizeState.PENDING;
    public int failReturnCount = 0;
    public int delayFrameCount = 0;

    public Person(Rect rect, Mat image)
    {
        UpdateFace(rect, image);
    }

    public void UpdateFace(Rect rect, Mat image)
    {
        //Debug.Log("Face found, create Texture2D.");
        Mat grayface = image.submat(rect);
        using (Mat rgbface = new Mat())
        {
            Imgproc.cvtColor(grayface, rgbface, Imgproc.COLOR_GRAY2BGRA);
            Texture2D textre2D = new Texture2D(rgbface.width(), rgbface.height(), TextureFormat.BGRA32, false, false);
            //Texture2D textre2D = new Texture2D(grayface.width(), grayface.height(), TextureFormat.RG16, false, false);
            Utils.fastMatToTexture2D(rgbface, textre2D);
            //Utils.fastMatToTexture2D(grayface, textre2D);
            face = ImageConversion.EncodeToJPG(textre2D, 100);
        }
    }
}
