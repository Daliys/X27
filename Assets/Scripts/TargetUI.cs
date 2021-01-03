using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TargetUI : MonoBehaviour
{

	public Transform target; // за кем следим
	public RectTransform marker; // объект Image UI

	public Sprite arrowSprite; // иконка когда цель за приделами экрана
	public Sprite upDownSprite; // иконка когда цель позади
	public Sprite targetSprite; // иконка когда цель в поле видимости

	public float minSize = 25; // размеры иконок
	public float maxSize = 50;

	private Camera _camera;
	private Vector3 newPos;
	private float upDown;

	void Awake()
	{
		_camera = Camera.main;
	}

	bool Behind(Vector3 point) // находится ли указанная точка позади нас
	{
		bool result = false;
		Vector3 forward = _camera.transform.TransformDirection(Vector3.forward);
		Vector3 toOther = point - _camera.transform.position;
		if (Vector3.Dot(forward, toOther) < 0) result = true;
		return result;
	}

	void LateUpdate()
	{
		Vector3 position = _camera.WorldToScreenPoint(target.position); // из мирового пространства в экранное
		Rect rect = new Rect(0, 0, Screen.width, Screen.height); // создаем окно
		newPos = position;
		upDown = 1;

		if (!Behind(target.position))
		{
			if (rect.Contains(position)) // если цель в окне экрана
			{
				marker.GetComponent<Image>().sprite = targetSprite;
			}
			else
			{
				marker.GetComponent<Image>().sprite = arrowSprite;
			}
		}
		else // если цель позади
		{
			position = -position;

			if (_camera.transform.position.y > target.position.y)
			{
				newPos = new Vector3(position.x, 0, 0); // если цель ниже камеры, закрепляем иконки снизу
			}
			else
			{
				// если цель выше камеры, закрепляем иконки вверху
				// и инвертируем угол поворота
				upDown = -1;
				newPos = new Vector3(position.x, Screen.height, 0);
			}

			marker.GetComponent<Image>().sprite = upDownSprite;
		}

		// закрепляем иконку в границах экрана с вычетом половины ее размера
		float size = marker.sizeDelta.x / 2;
		newPos.x = Mathf.Clamp(newPos.x, size, Screen.width - size);
		newPos.y = Mathf.Clamp(newPos.y, size, Screen.height - size);

		// находим угол вращения к цели
		Vector3 pos = position - newPos;
		float angle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
		marker.rotation = Quaternion.AngleAxis(angle * upDown, Vector3.forward);

		// изменение размера, относительно расставания
		float dis = Vector3.Distance(_camera.transform.position, target.position);
		float scale = maxSize - dis / 4;
		scale = Mathf.Clamp(scale, minSize, maxSize);
		marker.sizeDelta = new Vector2(scale, marker.sizeDelta.y);

		marker.anchoredPosition = newPos;
	}
}