using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class RagdollHandler : MonoBehaviour
{
	// Start is called before the first frame update
	public List<RagdollData> RagdollData = new List<RagdollData>();
	private Animator _animator;
	[SerializeField] private bool _canDisableCollider = true;
	private void Awake()
	{

		DisableRagdoll();
		_animator = GetComponentInChildren<Animator>();
	}

	[HideInPlayMode]
	[Button("Assign Ragdoll Data")]
	private void PrepareRagdollData()
	{
		RagdollData.Clear();
		Transform[] allChild = GetComponentsInChildren<Transform>();
		foreach (var child in allChild)
		{
			Collider col = child.GetComponent<Collider>();
			Rigidbody rb = child.GetComponent<Rigidbody>();
			CharacterJoint joint = child.GetComponent<CharacterJoint>();
			if (col != null && !(col is CharacterController))
			{
				RagdollData.Add(new RagdollData(col, rb, joint));
			}
		}
	}

	[DisableInEditorMode]
	[Button("Enable Ragdoll")]
	public void EnableRagdoll()
	{
		if (_animator != null)
		{
			_animator.enabled = false;
		}

		foreach (var item in RagdollData)
		{
			item.EnableRagdoll();
		}
	}

	[DisableInEditorMode]
	[Button("Disable Ragdoll")]
	public void DisableRagdoll()
	{
		foreach (var item in RagdollData)
		{
			item.DisableRagdoll(_canDisableCollider);
		}
	}

	[Button("Copy From Other Ragdoll Handler")]
	public void CopyFromOtherRagdollHandler(RagdollHandler handler)
	{
		Transform[] allChild = GetComponentsInChildren<Transform>();
		foreach (var otherRagdollData in handler.RagdollData)
		{
			var child = allChild.First(w => w.gameObject.name == otherRagdollData.Collider.gameObject.name);
			if (!child.TryGetComponent(out Collider col))
			{
				child.gameObject.AddComponent(otherRagdollData.Collider.GetType());
			}
		}
		PrepareRagdollData();
		for (int i = 0; i < RagdollData.Count; i++)
		{
			RagdollData[i].CopyColliderValuesFrom(handler.RagdollData[i].Collider);
		}
	}

	public void ApplyForceToAllBodyParts(float amount,Vector3 dir)
	{
		foreach (var data in RagdollData)
		{
			data.Rigidbody.AddForce((dir) * amount, ForceMode.VelocityChange);
		}
	}

	public Rigidbody GetRandomBodyPart()
	{
		return RagdollData[Random.Range(0, RagdollData.Count)].Rigidbody;
	}

	public void SetAllBodyPartVelocity(Vector3 velocity)
	{
		foreach (var data in RagdollData)
		{
			data.Rigidbody.velocity = velocity;
		}
	}
}

[System.Serializable]
public class RagdollData
{
	public Collider Collider;
	public Rigidbody Rigidbody;
	public CharacterJoint Joint;

	public RagdollData(Collider collider, Rigidbody rigidbody, CharacterJoint joint)
	{
		Collider = collider;
		Rigidbody = rigidbody;
		Joint = joint;
	}

	public void DisableRagdoll(bool canDisableCollider = true)
	{
		if (canDisableCollider)
		{
			Collider.enabled = false;
		}
		else
		{
			Collider.isTrigger = true;
		}
		if (Rigidbody != null)
		{
			Rigidbody.isKinematic = true;
		}
	}

	public void EnableRagdoll()
	{
		Collider.enabled = true;
		Collider.isTrigger = false;
		if (Rigidbody != null)
		{
			Rigidbody.velocity = Vector3.zero;
			Rigidbody.isKinematic = false;
		}
	}

	public void CopyColliderValuesFrom(Collider other)
	{
		if (other.GetType() == typeof(BoxCollider))
		{
			var myCol = (BoxCollider) Collider;
			var otherCol = (BoxCollider) other;
			myCol.center = otherCol.center;
			myCol.size = otherCol.size;
		}
		else if (other.GetType() == typeof(CapsuleCollider))
		{
			var myCol = (CapsuleCollider) Collider;
			var otherCol = (CapsuleCollider) other;
			myCol.center = otherCol.center;
			myCol.height = otherCol.height;
			myCol.direction = otherCol.direction;
			myCol.radius = otherCol.radius;
		}
		else if (other.GetType() == typeof(SphereCollider))
		{
			var myCol = (SphereCollider) Collider;
			var otherCol = (SphereCollider) other;
			myCol.center = otherCol.center;
			myCol.radius = otherCol.radius;
		}
	}
}