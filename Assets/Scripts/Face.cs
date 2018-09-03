namespace FaceDetection
{
    [System.Serializable]
    public class Face
    {

        public Face(string faceData, long identify)
        {
            this.FaceData = faceData;
            this.Identify = identify;
        }

        public Face(string faceData)
        {
            this.FaceData = faceData;
        }

        public Face()
        {
            
        }


        public string FaceData;
        public long Identify;
        public float Score;
    }
}
