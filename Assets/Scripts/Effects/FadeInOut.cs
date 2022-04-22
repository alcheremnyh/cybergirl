using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeInOut : MonoBehaviour
{

	private static bool sceneEnd;
	private float fadeSpeed = 3f;
	private int nextLevel;
	private Image _image;
	private bool sceneStarting;

	public void GotoScene(int sceneId)
    {
		sceneEnd = true;
		nextLevel = sceneId;
	}

	void Awake()
	{
		_image = GetComponent<Image>();
		_image.enabled = true;
		sceneStarting = true;
		sceneEnd = false;
		//Cursor.visible = false;
	}

	void Update()
	{
		if (sceneStarting) StartScene();
		if (sceneEnd) EndScene();
	}

	void StartScene()
	{
		_image.color = Color.Lerp(_image.color, Color.clear, fadeSpeed * Time.deltaTime);

		if (_image.color.a <= 0.01f)
		{
			_image.color = Color.clear;
			_image.enabled = false;
			sceneStarting = false;
			//Cursor.visible = true;
		}
	}

	void EndScene()
	{
		_image.enabled = true;
		_image.color = Color.Lerp(_image.color, Color.black, fadeSpeed * Time.deltaTime);

		if (_image.color.a >= 0.95f)
		{
			//Cursor.visible = false;
			_image.color = Color.black;
			SceneManager.LoadScene(nextLevel);
		}
	}
}
