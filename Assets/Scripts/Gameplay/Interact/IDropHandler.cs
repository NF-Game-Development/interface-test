using System.Collections.Generic;
using UnityEngine;

public interface IDropHandler
{ 
    void DropItem(); 
    GameObject GetRandomDrop(List<ItemDrop> drops);
}