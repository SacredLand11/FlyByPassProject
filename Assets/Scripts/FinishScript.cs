using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;

public class FinishScript : MonoBehaviour
{
    //Snapshot Mechanics
    [Header("Photo Mechanics")]
    public RenderTexture TargetTexture;
    public Camera SnapshotCamera;
    public string SnapshotName;
    public Texture2D CaptureTexture;
    public Canvas PhotoCanvas;
    bool photoBool = false;

    //Screen Becomes White for .2f sec (Flash Condition)
    [Header("Fader GameObject")]
    public GameObject faderPrefab;

    //Money Update
    [Header("Money Upgrade")]
    public Text moneyText;
    public int money = 0;
    private void Start()
    {
        moneyText = GameObject.Find("MoneyTextCanvas(Clone)").gameObject.transform.GetChild(0).GetComponent<Text>();
        SnapshotName = "Capture";
    }
    private void OnTriggerEnter(Collider other)
    {
        //Snapshot & Flash & Money Conditions
        if(other.gameObject.tag == "Player")
        {
            Instantiate(faderPrefab);
            money += 100;
            SnapshotMechanics();
            Instantiate(PhotoCanvas);
            photoBool = true;
        }
    }
    private void Update()
    {
        if (photoBool)
        {
            GameObject.Find("PhotoCanvas(Clone)").gameObject.transform.GetChild(2).GetComponent<RawImage>().texture = CaptureTexture;
        }
    }

    //Snapshot Function
    private void SnapshotMechanics()
    {
        Camera Scenecamera = SceneView.lastActiveSceneView.camera;
        SnapshotCamera.transform.SetPositionAndRotation(Scenecamera.transform.position, Scenecamera.transform.rotation);

        Scenecamera.Render();

        Texture2D TextureToSave = ToTexture2D(TargetTexture);
        byte[] ImageData = TextureToSave.EncodeToPNG();

        File.WriteAllBytes($"{Application.dataPath}/{SnapshotName}.png", ImageData);

        AssetDatabase.Refresh();
    }
    //Texture Function
    private Texture2D ToTexture2D(RenderTexture Target)
    {
        Texture2D Result = new Texture2D(Target.width, Target.height);
        RenderTexture.active = Target;

        Result.ReadPixels(new Rect(0, 0, Target.width, Target.height), 0, 0);
        Result.Apply();

        RenderTexture.active = null;

        return Result;
    }
}
