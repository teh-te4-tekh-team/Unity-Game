using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static Transform target;
    public float distance = 4.0f;
    public float height = 1.0f;
    public float smoothLag = 0.2f;
    public float maxSpeed = 10.0f;
    public float snapLag = 0.3f;
    public float clamHeadPositionScreenSpace = 0.75f;
    private LayerMask lineofSightMask = 0;
    private Vector3 headOffset = Vector3.zero;
    private Vector3 centerOffset = Vector3.zero;

    bool isSnapping = false;
    Vector3 velocity = Vector3.zero;
    float targetHeight = 100000.0f;

    void Apply(Transform dummyTarget, Vector3 dummyCenter)
    {
        Vector3 targetCenter = target.position + this.centerOffset;
        Vector3 targetHead = target.position + this.headOffset;

        this.targetHeight = targetCenter.y + this.height;

        if (Input.GetButton("Fire2") && !this.isSnapping)
        {
            this.velocity = Vector3.zero;
            this.isSnapping = true;
        }

        if (this.isSnapping)
        {
            this.ApplySnapping(targetCenter);
        }
        else
        {
            this.ApplyPositionDamping(new Vector3(targetCenter.x,
                this.targetHeight,
                targetCenter.z));
        }

        this.SetUpRotation(targetCenter, targetHead);
    }

    private void SetUpRotation(Vector3 centerPos, Vector3 headPos)
    {
        Vector3 cameraPos = this.transform.position;
        Vector3 offsetToCenter = centerPos - cameraPos;

        Quaternion yRotation = Quaternion.LookRotation(new Vector3(offsetToCenter.x, 0.0f, offsetToCenter.z));

        Vector3 relativeOffset = Vector3.forward * this.distance + Vector3.down * this.height;
        this.transform.rotation = yRotation * Quaternion.LookRotation(relativeOffset);

        Ray centerRay = this.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 1.0f));
        Ray topRay = this.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, this.clamHeadPositionScreenSpace, 1.0f));

        Vector3 centerRayPos = centerRay.GetPoint(this.distance);
        Vector3 topRayPos = topRay.GetPoint(this.distance);

        float centerToTopAngle = Vector3.Angle(centerRay.direction, topRay.direction);
        float heightToAngle = centerToTopAngle / (centerRayPos.y - topRayPos.y);
        float extraLookAngle = heightToAngle * (centerRayPos.y - centerPos.y);

        if (extraLookAngle >= centerToTopAngle)
        {
            extraLookAngle = extraLookAngle - centerToTopAngle;
            this.transform.rotation *= Quaternion.Euler(-extraLookAngle, 0, 0);
        }
    
    }

    private void ApplyPositionDamping(Vector3 targetCenter)
    {
        Vector3 position = this.transform.position;
        Vector3 offset = position - targetCenter;
        offset.y = 0;
        Vector3 newTargetPos = offset.normalized * this.distance + targetCenter;

        Vector3 newPosition;
        newPosition.x = Mathf.SmoothDamp(position.x, newTargetPos.x, ref this.velocity.x, this.smoothLag, this.maxSpeed);
        newPosition.y = Mathf.SmoothDamp(position.y, newTargetPos.y, ref this.velocity.y, this.smoothLag, this.maxSpeed);
        newPosition.z = Mathf.SmoothDamp(position.z, newTargetPos.z, ref this.velocity.z, this.smoothLag, this.maxSpeed);

        newPosition = this.AdjustLineOfSight(newPosition, targetCenter);

        this.transform.position = newPosition;
    }

    void LateUpdate()
    {
        if (target)
        {
            this.Apply(null, Vector3.zero);
        }
    }

    void ApplySnapping(Vector3 targetCenter)
    {
        Vector3 position = this.transform.position;
        Vector3 offset = position - targetCenter;
        offset.y = 0;
        float currentDistance = offset.magnitude;

        float targetAngle = target.eulerAngles.y;
        float currentAngle = this.transform.eulerAngles.y;

        currentAngle = Mathf.SmoothDampAngle(currentAngle, targetAngle, ref this.velocity.x, this.snapLag);
        currentDistance = Mathf.SmoothDamp(currentDistance, this.distance, ref this.velocity.z, this.snapLag);

        Vector3 newPosition = targetCenter;
        newPosition += Quaternion.Euler(0, currentAngle, 0) * Vector3.back * currentDistance;

        newPosition.y = Mathf.SmoothDamp(position.y, targetCenter.y + this.height, ref this.velocity.y, this.smoothLag,
            this.maxSpeed);

        newPosition = this.AdjustLineOfSight(newPosition, targetCenter);

        this.transform.position = newPosition;

        if (this.AngleDistance(currentAngle, targetAngle) < 3.0)
        {
            this.isSnapping = false;
            this.velocity = Vector3.zero;
        }
    }

    private float AngleDistance(float a, float b)
    {
        a = Mathf.Repeat(a, 360);
        b = Mathf.Repeat(b, 360);

        return Mathf.Abs(b - a);
    }

    Vector3 GetCenterOffset()
    {
        return this.centerOffset;
    }

    void SetTarget(Transform transform)
    {
        target = transform;
    }

    Vector3 AdjustLineOfSight(Vector3 newPosition, Vector3 target)
    {
        RaycastHit hit;
        if (Physics.Linecast(target, newPosition, out hit, this.lineofSightMask.value))
        {
            this.velocity = Vector3.zero;
            return hit.point;
        }
        return newPosition;
    }


}
