using DissassemblyLine.Interfaces;
using UnityEngine;

namespace DissassemblyLine
{
    public class Grinder : MonoBehaviour
    {

        [SerializeField] private AudioClip grindingSFX;

        private int grinderID = -1;


        private void Awake()
        {
            Debug.Assert(grindingSFX, name + " is missing grinding fx in the inspector!");
        }


        private void Start()
        {
            ConveyorBelt conveyorBelt = GetComponentInParent<ConveyorBelt>();

            Debug.Assert(conveyorBelt, "Unable to find conveyor belt on parent object for "+ name);

            if (conveyorBelt)
            {
                grinderID = conveyorBelt.ConveyorBeltID;
            }
        }


        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IGrindable grindable))
            {
                grindable.OnGrind();
                //EventManager.RaiseBodyGrindedEvent(deadBody.currentConveyorBeltID);
                if (grindingSFX) AudioSource.PlayClipAtPoint(grindingSFX, Camera.main.transform.position);
            }
        }
    }
}
