using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatheringUnitBehaviour : MonoBehaviour, IUnitBehaviour {

    private const int RESOURCE_CARRY_AMOUNT_MAX = 3;

    public event EventHandler OnStartGathering;
    public event EventHandler OnGoingToStorage;
    public event EventHandler OnGatheredResource;
    public event EventHandler OnStateBackToNormalMovement;


    private enum State {
        GoingToResourceNode,
        Gathering,
        GoingToStorage,
    }

    private PlayerController unit;
    private State state;
    private ResourceNode resourceNode;
    private Vector3 lastResourceNodePosition;
    private ResourceTypeSO lastResourceNodeResourceType;
    private float resourceGatherTimer;
    private Storage storage;
    private List<ResourceTypeSO> inventoryResourceTypeList;

    private void Awake() {
        unit = GetComponent<PlayerController>();

        inventoryResourceTypeList = new List<ResourceTypeSO>();

    }

    public void SetGatherResources(ResourceNode resourceNode) {
        
        unit.SetActiveBehaviour(this);
        SetResourceNode(resourceNode);
    }

    public void SetResourceNode(ResourceNode resourceNode) {

        
        this.resourceNode = resourceNode;
        lastResourceNodePosition = resourceNode.GetPosition();
        lastResourceNodeResourceType = resourceNode.GetResourceTypeSO();
        state = State.GoingToResourceNode;
    }

    private int GetTotalResourceAmount() {
        return inventoryResourceTypeList.Count;
    }

    public void UpdateBehaviour() {
        
        switch (state) {
            
            case State.GoingToResourceNode:
                if (TestFinishedGathering()) break;

                float reachedDistance = 3f;
                unit.SetDestination(resourceNode.GetPosition(), reachedDistance - .5f);

                if (Vector3.Distance(unit.GetPosition(), resourceNode.GetPosition()) < reachedDistance) {
                    // Reached!
                  
                    unit.StopMoving();
                    // Start Gathering
                    state = State.Gathering;
                    OnStartGathering?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.Gathering:
                if (TestFinishedGathering()) break;
                    Debug.Log("empezando a recoger recursos");
                resourceGatherTimer -= Time.deltaTime;
                if (resourceGatherTimer < 0) {
                    float resourceGatherTimerMax = 3f;
                    resourceGatherTimer += resourceGatherTimerMax;

                    inventoryResourceTypeList.Add(resourceNode.GetResourceTypeSO());
                    resourceNode.GrabResource();

                    OnGatheredResource?.Invoke(this, EventArgs.Empty);

                    if (GetTotalResourceAmount() >= RESOURCE_CARRY_AMOUNT_MAX || !resourceNode.HasResources()) {
                        TestFinishedGathering();
                    }
                }
                break;
            case State.GoingToStorage:
                Vector3 storagePosition = storage.GetPosition();

                reachedDistance = 3f;
                unit.SetDestination(storagePosition, reachedDistance - .5f);

                if (Vector3.Distance(unit.GetPosition(), storagePosition) < reachedDistance) {
                    // Reached!
                    unit.StopMoving();
                    // Drop Resources
                    /* EventManager.CallAddResourceAmountEvent(inventoryResourceTypeList); */
                    ResourceManager.Instance.AddResourceAmount(inventoryResourceTypeList);
                    inventoryResourceTypeList.Clear();
                    state = State.GoingToResourceNode;
                }
                break;
        }
    }

    private bool TryGoToStorage() {
        if (GetTotalResourceAmount() > 0) {
            // Has resources, drop them off
            storage = Storage.GetClosestStorage(unit.GetPosition());
            if (storage != null) {
                state = State.GoingToStorage;
                OnGoingToStorage?.Invoke(this, EventArgs.Empty);
                return true;
            } else {
                // No storage exists! Stop gathering!
                return false;
            }
        }

        return false;
    }

    private bool IsResourceNodeDead() {
        return resourceNode == null || !resourceNode.HasResources();
    }

    private bool TestFinishedGathering() {
        if (IsResourceNodeDead()) {
            // No more resources
            if (TryGoToStorage()) {
                // Heading to storage
                return true;
            } else {
                // Finished gathering, find other resource node
                if (TryFindClosestResourceNode()) {
                    // Found new resource node
                    return true;
                } else {
                    // No nearby resource nodes, back to normal
                    unit.NormalMoveTo(unit.GetPosition());
                    OnStateBackToNormalMovement?.Invoke(this, EventArgs.Empty);
                    return true;
                }
            }
        } else {
            // Node still has resources, is inventory full?
            if (GetTotalResourceAmount() >= RESOURCE_CARRY_AMOUNT_MAX) {
                // Full, try go to storage
                if (TryGoToStorage()) {
                    // Heading to storage
                    return true;
                } else {
                    // Cannot find nearby storage, back to normal
                    unit.NormalMoveTo(unit.GetPosition());
                    OnStateBackToNormalMovement?.Invoke(this, EventArgs.Empty);
                    return true;
                }
            } else {
                // Node still has resources and inventory not full, dont stop gathering
                return false;
            }
        }
    }

    private bool TryFindClosestResourceNode() {
        if (lastResourceNodeResourceType != null) {
            float findResourceNodeRange = 10f;
            Collider[] colliderArray = Physics.OverlapSphere(lastResourceNodePosition, findResourceNodeRange);
            ResourceNode closestResourceNode = null;
            foreach (Collider collider in colliderArray) {
                if (collider.TryGetComponent(out ResourceNode newResourceNode)) {
                    if (!newResourceNode.HasResources()) continue; // New node doesn't have resources
                    if (newResourceNode.GetResourceTypeSO() != lastResourceNodeResourceType) continue; // New node different Resource Type

                    if (closestResourceNode == null) {
                        closestResourceNode = newResourceNode;
                    } else {
                        if (Vector3.Distance(newResourceNode.GetPosition(), lastResourceNodePosition) <
                            Vector3.Distance(closestResourceNode.GetPosition(), lastResourceNodePosition)) {
                            // Closer
                            closestResourceNode = newResourceNode;
                        }
                    }
                }
            }

            if (closestResourceNode != null) {
                SetResourceNode(closestResourceNode);
                return true;
            }
        }

        return false;
    }

}
