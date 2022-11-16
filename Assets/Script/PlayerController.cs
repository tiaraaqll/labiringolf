using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Ball ball;
    [SerializeField] GameObject arrow;
    [SerializeField] Image aim;
    [SerializeField] LineRenderer line;
    [SerializeField] TMP_Text shootCountText;
    [SerializeField] LayerMask ballLayer;
    [SerializeField] LayerMask rayLayer;
    [SerializeField] Transform cameraPivot;
    [SerializeField] Camera cam;
    [SerializeField] Vector2 camSensitivity;
    [SerializeField] float shootForce;
    Vector3 lastMousePosition;
    float ballDistance;
    bool isShooting;
    float forceFactor;
    Vector3 forceDir;
    Renderer[] arrowRends;
    int shootCount = 0;

    public int ShootCount {get => shootCount;}

    //membuat warna arrow menjadi 3
    //Color[] arrowOriginalColors;
    
   private void Start()
   {
        ballDistance = Vector3.Distance(cam.transform.position, ball.Position) + 1;
        arrowRends = arrow.GetComponentsInChildren <Renderer> ();

        //membuat warna arrow jadi 3
        // arrowOriginalColors = new Color[arrowRends.Lenght];
        // for (int i = 0; i < arrowRends.Lenght; i++)
        // {
        //    arrowOriginalColors[i] = arrowRends[i].material.color;
        // }
        

        //sembunyikan arrow
        arrow.SetActive(false);

        //membuat tulisan shoot count (TMP)
        shootCountText.text = "Shoot Count : " + shootCount;
   }
    void Update()
    {
        //kondisi ketika bola bergerak, tidak bisa melakukan hal lain
        if(ball.IsMoving || ball.IsTeleporting)
            return;

        if(this.transform.position != ball.Position)  
            {
                this.transform.position = ball.Position;
                aim.gameObject.SetActive(true);
                var rect = aim.GetComponent <RectTransform> ();
                rect.anchoredPosition = cam.WorldToScreenPoint(ball.Position);

            }

        //controller mengikuti posisi ball
        this.transform.position = ball.Position;

        //untuk menembak
        if(Input.GetMouseButtonDown(0)) { 
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, ballDistance, ballLayer))
                {
                    isShooting = true;
                    arrow.SetActive(true);
                }
        }

        //untuk munculin kursor menembak (Shooting mode)
        if(Input.GetMouseButton(0) && isShooting == true) 
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, ballDistance * 2, rayLayer)) 
            {
                Debug.DrawLine(ball.Position, hit.point);
                
                //jarak satuan unit
                var forceVector = ball.Position - hit.point;
                forceVector = new Vector3(forceVector.x, 0, forceVector.z);
                forceDir = forceVector.normalized;
                var forceMagnitude = forceVector.magnitude; 
                Debug.Log(hit.point);
                forceMagnitude = Mathf.Clamp(forceMagnitude, 0, 5);
                forceFactor = forceMagnitude/5;
            }

            //arow
            this.transform.LookAt(this.transform.position + forceDir);
            arrow.transform.localScale = new Vector3 (
                1 + 0.5f * forceFactor, 
                1 + 0.5f * forceFactor, 
                1 + 2 * forceFactor);

            //ubah warna
            foreach (var rend in arrowRends)
            {
                rend.material.color = Color.Lerp(Color.yellow, Color.red, forceFactor);
            }

            //aim (posisi merah untuk mengeker)
            var rect = aim.GetComponent <RectTransform> ();
            rect.anchoredPosition = Input.mousePosition;

            //line
            var ballScrPos = cam.WorldToScreenPoint(ball.Position);
            line.SetPositions(new Vector3[] {ballScrPos, Input.mousePosition});
            
            //membuat warna arrow menjadi 3
            // for(int i = 0; i < rend in arrowRends.Length; i++)
            // {
            //      ubah warna dari texture
            //     arrowRends[i].material.color = Color.Lerp(arrowOriginalColors[i], Color.red, forceFactor);
            // }
        }

        //camera mode
        if (Input.GetMouseButton(0) && isShooting == false) {//0 itu klik kiri
            var current = cam.ScreenToViewportPoint(Input.mousePosition);
            var last = cam.ScreenToViewportPoint(lastMousePosition);

            //untuk merubah posisi
            var delta = current - last;

            // //untuk mengetahui seberapa jauh jarak pandang (bernilai positif)
            // var delta = dragDir.magnitude;
            
            //rotate around = titik center bisa dirubah, camera bisa melihat sekeliling
            cameraPivot.transform.RotateAround(//horizontal
                ball.Position, //titik tengah di bola
                Vector3.up, //berputar dari atas bola
                delta.x * camSensitivity.x); //berputar ke dunia searah jarum jam

            cameraPivot.transform.RotateAround(//vertical
                ball.Position, //titik tengah di bola
                cam.transform.right, //berputar dari kanan kamera
                - delta.y * camSensitivity.y); //kberputar ke atas bawah

            //posisi kamera
            var angle = Vector3.SignedAngle(Vector3.up, cam.transform.up, cam.transform.right);
            //Debug.Log(angle);

            //kalau melewati batas putar balik
            if(angle < 3)
                cameraPivot.transform.RotateAround(//vertical
                    ball.Position, //titik tengah di bola
                    cam.transform.right, //berputar dari kanan kamera
                    3 - angle);
            else if(angle > 65)
                cameraPivot.transform.RotateAround(//vertical
                    ball.Position, //titik tengah di bola
                    cam.transform.right, //berputar dari kanan kamera
                    65 - angle);
        }

        if(Input.GetMouseButtonUp(0) && isShooting) {
            ball.AddForce(forceDir * shootForce * forceFactor);
            shootCount += 1;
            shootCountText.text = "Shoot Count : " + shootCount;
            forceFactor = 0;
            forceDir = Vector3.zero;
            isShooting = false;
            arrow.SetActive(false);
        }
        lastMousePosition = Input.mousePosition;
    }
}
