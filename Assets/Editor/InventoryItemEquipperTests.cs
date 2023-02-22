using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class InventoryItemEquipperTests
{

    [Test]
    [Category("Equipment")]
    public void InitItems_True_NoItemsEquippedOnHeroByDefault()
    {
        //Equipment:
        //Input: null

        //Output:
        //Slot HEAD = null
        //Slot BODY = null
        //Slot HANDS = null
        //Slot RIGHT_HAND = null
        //Slot LEFT_HAND = null
        //Slot LEGS = null

        //Arrange:
        InventoryItemEquipper equipper = new InventoryItemEquipper();

        equipper.Equipment = new Dictionary<EquipType, InventoryItem>()
        {
            {EquipType.HEAD, null },
            {EquipType.BODY, null },
            {EquipType.HANDS, null },
            {EquipType.LEFT_HAND, null },
            {EquipType.RIGHT_HAND, null },
            {EquipType.LEGS, null }
        };

        //Act:
        equipper.InitEquipment();

        var itemOnHead = equipper.GetEquippedItem(EquipType.HEAD);
        var itemOnBody = equipper.GetEquippedItem(EquipType.BODY);
        var itemOnHands = equipper.GetEquippedItem(EquipType.HANDS);
        var itemOnRHand = equipper.GetEquippedItem(EquipType.RIGHT_HAND);
        var itemOnLHand = equipper.GetEquippedItem(EquipType.LEFT_HAND);
        var itemOnLegs = equipper.GetEquippedItem(EquipType.LEGS);

        //Assert:
        Assert.AreEqual(null, itemOnHead);
        Assert.AreEqual(null, itemOnBody);
        Assert.AreEqual(null, itemOnHands);
        Assert.AreEqual(null, itemOnRHand);
        Assert.AreEqual(null, itemOnLHand);
        Assert.AreEqual(null, itemOnLegs);
    }

    [Test]
    [Category("Equipment")]
    public void EquipItem_True_ItemCanEquipToHero()
    {
        //Inventory:

        //Input:
        //SimpleBoots = 1

        //Output:
        //SimpleBoots = 1

        //Equipment:

        //Input:
        //Slot HEAD = null
        //Slot BODY = null
        //Slot HANDS = null
        //Slot RIGHT_HAND = null
        //Slot LEFT_HAND = null
        //Slot LEGS = null

        //Output:
        //Slot HEAD = null
        //Slot BODY = null
        //Slot HANDS = null
        //Slot RIGHT_HAND = null
        //Slot LEFT_HAND = null
        //Slot LEGS = null

        //Arrange
        const string itemName = "SimpleBoots";
        InventoryItemEquipper equipper = new InventoryItemEquipper();

        equipper.Equipment = new Dictionary<EquipType, InventoryItem>()
        {
            {EquipType.HEAD, null },
            {EquipType.BODY, null },
            {EquipType.HANDS, null },
            {EquipType.LEFT_HAND, null },
            {EquipType.RIGHT_HAND, null },
            {EquipType.LEGS, null }
        };

        var inventory = new StackableInventory();
        equipper.SetInventory(inventory);

        var bootsItemConfig = ScriptableObject.CreateInstance<InventoryItemConfig>();
        bootsItemConfig.Prototype = new InventoryItem(itemName, InventoryItemFlags.EQUIPPABLE, metadata: null,
            new Component_EquipType(type: EquipType.LEGS));


        inventory.AddItemAsInstance(bootsItemConfig.Prototype);

        //Act

        //Assert
        Assert.True(equipper.CanEquipItem(bootsItemConfig.Prototype));
    }

    [Test]
    [Category("Equipment")]
    public void EquipItem_True_ItemEquippedToHero()
    {
        //Inventory:

        //Input:
        //SimpleBoots = 1

        //Output:
        //SimpleBoots = 0

        //Equipment:

        //Input:
        //Slot HEAD = null
        //Slot BODY = null
        //Slot HANDS = null
        //Slot RIGHT_HAND = null
        //Slot LEFT_HAND = null
        //Slot LEGS = null

        //Output:
        //Slot HEAD = null
        //Slot BODY = null
        //Slot HANDS = null
        //Slot RIGHT_HAND = null
        //Slot LEFT_HAND = null
        //Slot LEGS = SimpleBoots = 1

        //Arrange
        const string itemName = "SimpleBoots";
        InventoryItemEquipper equipper = new InventoryItemEquipper();

        equipper.Equipment = new Dictionary<EquipType, InventoryItem>()
        {
            {EquipType.HEAD, null },
            {EquipType.BODY, null },
            {EquipType.HANDS, null },
            {EquipType.LEFT_HAND, null },
            {EquipType.RIGHT_HAND, null },
            {EquipType.LEGS, null }
        };

        var inventory = new StackableInventory();
        equipper.SetInventory(inventory);

        var bootsItemConfig = ScriptableObject.CreateInstance<InventoryItemConfig>();
        bootsItemConfig.Prototype = new InventoryItem(itemName, InventoryItemFlags.EQUIPPABLE, metadata: null,
            new Component_EquipType(type: EquipType.LEGS));
       
        inventory.AddItemAsInstance(bootsItemConfig.Prototype);

        //Act
        equipper.EquipItem(bootsItemConfig.Prototype);

        //Assert
        Assert.False(inventory.IsItemExists(bootsItemConfig.Prototype));
        Assert.AreEqual(bootsItemConfig.Prototype, equipper.GetEquippedItem(EquipType.LEGS));
    }



    [Test]
    [Category("Equipment")]
    public void UnequipItem_True_ItemUnequippedFromHero()
    {
        //Inventory:

        //Input:
        //SimpleBoots = 0

        //Output:
        //SimpleBoots = 1

        //Equipment:

        //Input:
        //Slot HEAD = null
        //Slot BODY = null
        //Slot HANDS = null
        //Slot RIGHT_HAND = null
        //Slot LEFT_HAND = null
        //Slot LEGS = SimpleBoots = 1

        //Output:
        //Slot HEAD = null
        //Slot BODY = null
        //Slot HANDS = null
        //Slot RIGHT_HAND = null
        //Slot LEFT_HAND = null
        //Slot LEGS = null

        //Arrange
        const string itemName = "SimpleBoots";
        InventoryItemEquipper equipper = new InventoryItemEquipper();

        var inventory = new StackableInventory();

        equipper.SetInventory(inventory);

        var bootsItemConfig = ScriptableObject.CreateInstance<InventoryItemConfig>();
        bootsItemConfig.Prototype = new InventoryItem(itemName, InventoryItemFlags.EQUIPPABLE, metadata: null,
            new Component_EquipType(type: EquipType.LEGS));


        equipper.Equipment = new Dictionary<EquipType, InventoryItem>()
        {
            {EquipType.HEAD, null },
            {EquipType.BODY, null },
            {EquipType.HANDS, null },
            {EquipType.LEFT_HAND, null },
            {EquipType.RIGHT_HAND, null },
            {EquipType.LEGS, bootsItemConfig.Prototype }
        };        

        //Act
        equipper.UnequipItem(EquipType.LEGS);

        //Assert        
        Assert.AreEqual(null, equipper.GetEquippedItem(EquipType.LEGS));
        Assert.True(inventory.IsItemExists(bootsItemConfig.Prototype));
    }

    



    //[Test]
    //[Category("Inventory")]
    //[TestCase(50, 60, 50)]
    //[TestCase(100, 80, 20)]
    //[TestCase(201, 1200, 201)]
    //public void DebitGold_True_GoldNeverBelowZero(int deposit, int debit, int expected)
    //{
    //    //Arrange
    //    Inventory inventory = new Inventory();

    //    //Act
    //    inventory.DepositGold(deposit);
    //    inventory.DebitGold(debit);

    //    //Assert
    //    Assert.AreEqual(expected, inventory.Gold);
    //}




    //[Test]
    //[Category("Inventory")]
    //public void AddItem_True_ItemAddedToInventory()
    //{
    //    //Arrange
    //    Inventory inventory = new Inventory();
    //    Item item = new Item();

    //    //Act
    //    inventory.AddItem(item);

    //    //Assert
    //    Assert.AreEqual(item, inventory.Items[0]);
    //}

    //[Test]
    //[Category("Inventory")]
    //public void RemoveItem_True_ItemRemovedFromInventory()
    //{
    //    //Arrange
    //    Inventory inventory = new Inventory();
    //    Item item = new Item();

    //    //Act
    //    inventory.AddItem(item);
    //    inventory.RemoveItem(item);

    //    //Assert
    //    Assert.AreEqual(0, inventory.Items.Count);
    //}

    //[Test]
    //[Category("Inventory")]
    //public void CheckForEmptySlot_True_EmptySlotAvailable()
    //{
    //    //Arrange
    //    Inventory inventory = new Inventory();
    //    inventory.MaxSize = 5;

    //    //Act
    //    for (int i = 0; i < 4; i++)
    //    {
    //        inventory.AddItem(new Item());
    //    }

    //    //Assert
    //    Assert.IsTrue(inventory.CheckForEmptySlot());
    //}
}
