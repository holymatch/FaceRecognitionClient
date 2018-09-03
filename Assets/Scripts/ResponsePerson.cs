using FaceDetection;

namespace RestfulClient
{
    [System.Serializable]
    public class Person
    {
        public long Id;
        public string Name;
        public string Detail;
        public Face Face;

        public Person()
        {

        }
        public Person(string name, string detail, Face face)
        {
            //this.id = null;
            this.Name = name;
            this.Detail = detail;
            this.Face = face;
        }
    }
}
