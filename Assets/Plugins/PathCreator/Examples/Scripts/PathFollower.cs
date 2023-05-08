using UnityEngine;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 5;
        float distanceTravelled;

        void Start() {
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
            }
        }

        void Update()
        {
            if (pathCreator != null)
            {
                distanceTravelled += speed * Time.deltaTime;
                //transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                Vector3 target = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                // get the angle
                Vector3 norTar = (target - transform.position).normalized;
                float angle = Mathf.Atan2(norTar.y, norTar.x) * Mathf.Rad2Deg;
                // rotate to angle
                Quaternion rotation = new Quaternion();
                rotation.eulerAngles = new Vector3(0, 0, angle - 90);
                transform.rotation = rotation;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            }
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged() {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }
    }
}