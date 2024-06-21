using Leopotam.Ecs;
using UnityEngine;

public class TransferSystem : IEcsRunSystem
{
    private EcsFilter<Transfer> transterFilter;
    
    public void Run()
    {
        foreach (int i in transterFilter)
        {
            ref Transfer transfer = ref transterFilter.Get1(i);
            
            if (transfer.transferMarker)
            {
                void TransferItem(ref Transfer _transfer, ref Transfer _target)
                {
                    _transfer.currentItem.prefab.transform.SetParent(_target.transferPoint[_target.items.Count]);
                    _transfer.currentItem.prefab.transform.localPosition = Vector3.zero;
                    _transfer.currentItem.prefab.transform.localRotation = Quaternion.identity;
                    _transfer.transferMarker = false;
                }
                
                if (transfer.giver && transfer.items.Count > 0)
                {
                    ref Transfer toEntity = ref transfer.toEntity.Get<Transfer>();

                    if (toEntity.items.Count >= toEntity.maxItems)
                    {
                        transfer.transferMarker = false;
                        break;
                    }
                    
                    transfer.currentItem = transfer.items.Pop();
                    TransferItem(ref transfer, ref toEntity);
                    toEntity.items.Push(transfer.currentItem);
                }
                else if (transfer.taker)
                {
                    ref Transfer fromEntity = ref transfer.fromEntity.Get<Transfer>();
                    
                    if (fromEntity.items.Count < 1 || transfer.items.Count >= transfer.maxItems)
                    {
                        transfer.transferMarker = false;
                        break;
                    }
                    
                    transfer.currentItem = fromEntity.items.Pop();
                    TransferItem(ref transfer, ref transfer);
                    transfer.items.Push(transfer.currentItem);
                }
            }
        }
    }
}