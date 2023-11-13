using GorillaLocomotion.Climbing;
using UnityEngine;

public class Keys : MonoBehaviour
{
    public string key;
    public GameObject drone;

 

    public bool buttonlclicked;


    private float touchTime = 0f;
    private const float debounceTime = 0.25f;

    private const float horizontalMultiplier = 60f, verticalMultiplier = 50f;

    void Start()
    {
        
        key = this.transform.name;

        Destroy(GetComponent<Collider>());
        BoxCollider collider = gameObject.AddComponent<BoxCollider>();
        collider.isTrigger = true;

    }


    private void OnTriggerEnter(Collider other)
    {
        if (touchTime + debounceTime >= Time.time) return;

        if (other.TryGetComponent(out GorillaTriggerColliderHandIndicator component) && !component.isLeftHand)
        {
            GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(211, component.isLeftHand, 0.12f);
            GorillaTagger.Instance.StartVibration(component.isLeftHand, GorillaTagger.Instance.tapHapticStrength / 2f, GorillaTagger.Instance.tapHapticDuration);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (touchTime + debounceTime >= Time.time) return;

        if (other.TryGetComponent(out GorillaTriggerColliderHandIndicator component) && !component.isLeftHand )
        {
            touchTime = Time.time;

            GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(212, component.isLeftHand, 0.12f);
            GorillaTagger.Instance.StartVibration(component.isLeftHand, GorillaTagger.Instance.tapHapticStrength / 2f, GorillaTagger.Instance.tapHapticDuration);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out GorillaTriggerColliderHandIndicator component) && !component.isLeftHand )
        {


            if (key == "forward")
            {
                drone.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * horizontalMultiplier, ForceMode.Acceleration);
            }
            if (key == "left")
            {
                drone.GetComponent<Rigidbody>().AddRelativeForce(Vector3.left * horizontalMultiplier, ForceMode.Acceleration);
            }
            if (key == "right")
            {
                drone.GetComponent<Rigidbody>().AddRelativeForce(Vector3.right * horizontalMultiplier, ForceMode.Acceleration);
            }
            if (key == "backward")
            {
                drone.GetComponent<Rigidbody>().AddRelativeForce(Vector3.back * horizontalMultiplier, ForceMode.Acceleration);
            }
            if (key == "up key")
            {
                drone.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * verticalMultiplier, ForceMode.Acceleration);
            }
            if (key == "down key")
            {
                drone.GetComponent<Rigidbody>().AddRelativeForce(Vector3.down * verticalMultiplier, ForceMode.Acceleration);
            }
            if (key == "left turn")
            {
                drone.transform.Rotate(0, -1f, 0);
            }
            if (key == "right turn")
            {
                drone.transform.Rotate(0, 1f, 0);
            }



        }
    }

}


