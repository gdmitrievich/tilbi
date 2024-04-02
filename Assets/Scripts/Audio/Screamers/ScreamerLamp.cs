using UnityEngine;

public class ScreamerLamp : MonoBehaviour
{
	private AudioSource _audioSource;

	[SerializeField] private AudioClip _screamerSound;
	[SerializeField] private AudioClip _idleSound;

	[SerializeField] private Material _offMaterial;
	[SerializeField] private Material _onMaterial;

	private Renderer _lampRenderer;
	private bool _screamerSoundPlayed;

	private Light _light;

	void Start()
	{
		_audioSource = GetComponent<AudioSource>();

		_light = GetComponentInChildren<Light>();
		_light.enabled = false;

		ReloadMaterials(_offMaterial);

		_screamerSoundPlayed = false;
	}

	void Update() {
		if (_screamerSoundPlayed && !_audioSource.isPlaying) {
			_audioSource.loop = true;
			_audioSource.clip = _idleSound;
			_audioSource.Play();
			enabled = false;
		}
	}

	private void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == "Player" && !_screamerSoundPlayed)
		{
			ReloadMaterials(_onMaterial);
			_audioSource.PlayOneShot(_screamerSound);

			_light.enabled = true;
			_screamerSoundPlayed = true;
		}
	}

	void ReloadMaterials(Material material)
	{
		_lampRenderer = GetComponentInChildren<Renderer>();
		Material[] newLampMaterials = new Material[_lampRenderer.materials.Length];

		newLampMaterials[0] = _lampRenderer.materials[0];
		newLampMaterials[1] = material;

		_lampRenderer.materials = newLampMaterials;
	}
}