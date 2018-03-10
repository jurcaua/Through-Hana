using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour {

    public enum Mode {Mouse, Player};
    public Mode drawMode = Mode.Mouse;
    public PlayerController player;
    private LayerMask whatIsCloud;

    public Camera _camera;
    public Shader _drawShader;

    private RenderTexture _splatMap;
    private Material _snowMaterial, _drawMaterial;
    private RaycastHit _hit;
    
	void Start () {
        _drawMaterial = new Material(_drawShader);
        _drawMaterial.SetVector("_Color", Color.red);

        _snowMaterial = GetComponent<MeshRenderer>().material;
        _splatMap = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat);
        _snowMaterial.SetTexture("_Splat", _splatMap);

        whatIsCloud = LayerMask.GetMask("Cloud");
    }
	
	void Update () {
        if (drawMode == Mode.Mouse) {
            if (Input.GetKey(KeyCode.Mouse0)) {
                if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out _hit, whatIsCloud)) {
                    _drawMaterial.SetVector("_Coordinate", new Vector4(_hit.textureCoord.x, _hit.textureCoord.y, 0f, 0f));
                    RenderTexture temp = RenderTexture.GetTemporary(_splatMap.width, _splatMap.height, 0, RenderTextureFormat.ARGBFloat);
                    Graphics.Blit(_splatMap, temp);
                    Graphics.Blit(temp, _splatMap, _drawMaterial);
                    RenderTexture.ReleaseTemporary(temp);
                }
            }
        } else if (drawMode == Mode.Player) {
            Vector3 currentPosition = player.GetPosition();
            float intensity = Mathf.Clamp01((1 - (currentPosition.y - transform.position.y)));
            if (intensity > 0 && Physics.Raycast(new Ray(currentPosition, -player.transform.up), out _hit, whatIsCloud)) {
                Debug.Log(_hit.collider.name);
                _drawMaterial.SetVector("_Coordinate", new Vector4(_hit.textureCoord.x, _hit.textureCoord.y, 0f, 0f));
                _drawMaterial.SetFloat("_Intensity", intensity);
                RenderTexture temp = RenderTexture.GetTemporary(_splatMap.width, _splatMap.height, 0, RenderTextureFormat.ARGBFloat);
                Graphics.Blit(temp, _splatMap, _drawMaterial);
                RenderTexture.ReleaseTemporary(temp);
            } else {
                //_splatMap = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat);
            }
        }
	}

    private void OnGUI() {
        GUI.DrawTexture(new Rect(0, 0, 50, 50), _splatMap, ScaleMode.ScaleToFit, false, 1);
    }
}
