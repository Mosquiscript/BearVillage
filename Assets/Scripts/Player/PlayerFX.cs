using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFX : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerAnim animEvents;
    [SerializeField] private ParticleSystem slashEffect;    
    [SerializeField] private ParticleSystem reverseSlashEffect;
    [SerializeField] private ParticleSystem walkingParticle;
    [SerializeField] private GameObject selectedGameObject;

    private PlayerController unit;

    private void Awake() {
        unit = GetComponent<PlayerController>();
    }

    private void Start() {
        slashEffect.Stop();
        reverseSlashEffect.Stop();
        UpdateSelected();
        unit.OnIsSelectedChanged += Unit_OnIsSelectedChanged;
        unit.OnStartedMoving += Unit_OnStartedMoving;
        unit.OnStoppedMoving += Unit_OnStoppedMoving;

        if (TryGetComponent<GatheringUnitBehaviour>(out GatheringUnitBehaviour gatheringUnitBehaviour)) {
            gatheringUnitBehaviour.OnStartGathering += ChopChopUnitVisual_OnStartGathering;
            gatheringUnitBehaviour.OnGatheredResource += ChopChopUnitVisual_OnGatheredResource;
            gatheringUnitBehaviour.OnGoingToStorage += ChopChopUnitVisual_OnGoingToStorage;
            gatheringUnitBehaviour.OnStateBackToNormalMovement += ChopChopUnitVisual_OnStateBackToNormalMovement;
        }

       /*  if (TryGetComponent(out AttackingUnitBehaviour attackingUnitBehaviour)) {
            attackingUnitBehaviour.OnAttacking += (object sender, EventArgs e) => AnimatorIsAttacking(true);
        }

        if (TryGetComponent(out RangedAttackingUnitBehaviour rangedAttackingUnitBehaviour)) {
            rangedAttackingUnitBehaviour.OnRangedAttacking += (object sender, EventArgs e) => AnimatorIsAttacking(true);
        }

        if (TryGetComponent(out ConstructionUnitBehaviour constructionUnitBehaviour)) {
            constructionUnitBehaviour.OnStartConstructing += (object sender, EventArgs e) => AnimatorIsAttacking(true);
        }

        animEvents.OnPlayReverseSlashEffect += AnimEvents_OnPlayReverseSlashEffect;
        animEvents.OnPlaySlashEffect += AnimEvents_OnPlaySlashEffect; */
    }

    private void AnimEvents_OnPlaySlashEffect(object sender, System.EventArgs e) {
        slashEffect.Play();
    }

    private void AnimEvents_OnPlayReverseSlashEffect(object sender, System.EventArgs e) {
        reverseSlashEffect.Play();
    }

    private void ChopChopUnitVisual_OnStateBackToNormalMovement(object sender, System.EventArgs e) {
        if (animator != null) {
            animator.SetBool("IsAttacking", false);
        }
    }

    private void ChopChopUnitVisual_OnGoingToStorage(object sender, System.EventArgs e) {
        if (animator != null) {
            animator.SetBool("IsAttacking", false);
        }
    }

    private void ChopChopUnitVisual_OnStartGathering(object sender, System.EventArgs e) {
        if (animator != null) {
            animator.SetBool("IsAttacking", true);
        }
    }

    private void Unit_OnStoppedMoving(object sender, System.EventArgs e) {
        walkingParticle.Stop();
        if (animator != null) {
            animator.SetBool("IsWalking", false);
        }
    }

    private void Unit_OnStartedMoving(object sender, System.EventArgs e) {
        walkingParticle.Play();
        if (animator != null) {
            animator.SetBool("IsAttacking", false);
            animator.SetBool("IsGathered", false);
            animator.SetBool("IsWalking", true);
            animator.SetFloat("MovingSpeed", 1f);
        }
    }

    private void Unit_OnIsSelectedChanged(object sender, System.EventArgs e) {
        UpdateSelected();
    }

    private void ChopChopUnitVisual_OnGatheredResource(object sender, System.EventArgs e) {
        if (animator != null) {
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsGathered", true);
        }
    }

    private void UpdateSelected() {
        selectedGameObject.SetActive(unit.GetIsSelected());
    }

    private void AnimatorIsAttacking(bool _b) {
        if (animator != null) {
            animator.SetBool("IsAttacking", _b);
        }
    }
}
