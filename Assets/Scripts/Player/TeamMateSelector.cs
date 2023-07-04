
using UnityEngine;

public class TeamMateSelector : MonoBehaviour
{
    [SerializeField] TeamMember member;
    [SerializeField] new SphereCollider collider;

    private void OnMouseDown()
    {
        Player.OnTeamMemberSelected.Invoke(member);
    }

    public void SetRadius(float radius) => collider.radius = radius;
}
