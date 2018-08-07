// attached to the AnimPlayer Gameobject

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;


public class GetOnePixelFromVideo : MonoBehaviour
{
    public VideoClip videoToPlay1;
    VideoClip videoToPlay;
    private Color targetColor;
    private VideoPlayer videoPlayer;
    private VideoSource videoSource;
    private Renderer rend;
    public GameObject textureToRender;
    private Texture tex;
    private AudioSource audioSource;
    public Image UI_light;
    Texture2D videoFrame;
    public Dmx_Configurator dmxConfigurator;
    public VideoClip[] videoclips;
    public extOSC.Examples.OSC_send_receive_script oscSender;


    void OnEnable()
    {
        videoToPlay = videoToPlay1;
        videoFrame = new Texture2D(2, 2);
        Application.runInBackground = true;

        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.source = VideoSource.VideoClip;
        videoPlayer.clip = videoToPlay;
        videoPlayer.Prepare();

        StartCoroutine(PrepareVideo());

        rend = textureToRender.GetComponent<Renderer>();

        //Enable new frame Event
        videoPlayer.sendFrameReadyEvents = true;

        //Subscribe to the new frame Event
        videoPlayer.frameReady += OnNewFrame;

        //Play Video
        videoPlayer.Play();


    }

    // void Start(){

    //     btn_Anim(0);
    // }

    IEnumerator PrepareVideo()
    {
        //Wait until video is prepared
        while (!videoPlayer.isPrepared)
        {
            Debug.Log("Preparing Video");
            yield return null;
        }
        Debug.Log("Done Preparing Video");

        //Assign the Texture from Video to Material texture
        tex = videoPlayer.texture;
        // image.texture = videoPlayer.texture;
        rend.material.mainTexture = tex;


    }



    void OnNewFrame(VideoPlayer source, long frameIdx)
    {
        RenderTexture renderTexture = source.texture as RenderTexture;

        if (videoFrame.width != renderTexture.width || videoFrame.height != renderTexture.height)
        {
            videoFrame.Resize(renderTexture.width, renderTexture.height);
        }
        RenderTexture.active = renderTexture;
        videoFrame.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        videoFrame.Apply();
        RenderTexture.active = null;

        var pixelColor = videoFrame.GetPixel(Mathf.FloorToInt(5), Mathf.FloorToInt(2));
        var lightIntensity = pixelColor.r;

        // send value to light and to a UI-Light (Image) in Dmx_Configurator.cs

        Color debugLightColor = new Color(
            (float)lightIntensity * 1,
            (float)lightIntensity * 1,
            (float)lightIntensity * 1,
            1);
        UI_light.color = debugLightColor;

        dmxConfigurator.color_skypanel1 = debugLightColor;
        dmxConfigurator.color_skypanel2 = debugLightColor;
        dmxConfigurator.color_stripe = debugLightColor;
        // dmxConfigurator.color_toilette = debugLightColor;
        oscSender.color_toilette = debugLightColor;
    }

    public void btn_Anim(int pos)
    {
        // print("huhu anim");
        videoPlayer.clip = videoclips[pos];
        StartCoroutine(PrepareVideo());
        videoPlayer.Play();
    }
}
