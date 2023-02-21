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
        //Arrange:
        InventoryItemEquipper equipper = new InventoryItemEquipper();

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

        //Assert.AreEqual(false, equipper.IsItemEquipped(EquipType.HEAD));
        //Assert.AreEqual(false, equipper.IsItemEquipped(EquipType.BODY));
        //Assert.AreEqual(false, equipper.IsItemEquipped(EquipType.HANDS));
        //Assert.AreEqual(false, equipper.IsItemEquipped(EquipType.LEFT_HAND));
        //Assert.AreEqual(false, equipper.IsItemEquipped(EquipType.RIGHT_HAND));
        //Assert.AreEqual(false, equipper.IsItemEquipped(EquipType.LEGS));
    }

       
    
    //[Test]
    //[Category("Equipment")]
    //public void EquipItem_True_ItemEquippedToHero()
    //{
    //    //Arrange
    //    const string itemName = "sample";
    //    InventoryItemEquipper equipper = new InventoryItemEquipper();
    //    InventoryItem item = new InventoryItem(itemName, InventoryItemFlags.EQUIPPABLE, metadata: null);

    //    //Act
    //    equipper.EquipItem(item);

    //    //Assert
    //    Assert.AreEqual(item, equipper.Equipment[0]);
    //    //Assert.AreEqual(null, equipper.IsItemEquipped(item.GetComponent<IComponent_GetEqupType>().Type));

    //}

    //[Test]
    //[Category("Inventory")]
    //public void DepositGold_True_DepositedGold()
    //{
    //    //Arrange
    //    Inventory inventory = new Inventory();
    //    int expected = 10;

    //    //Act
    //    inventory.DepositGold(10);

    //    //Assert
    //    Assert.AreEqual(expected, inventory.Gold);
    //}
    //[Test]
    //[Category("Inventory")]
    //[TestCase(100)]
    //[TestCase(500)]
    //public void DepositGold_True_DoesntExceedMaxGold(int amount)
    //{
    //    //Arrange
    //    Inventory inventory = new Inventory();

    //    //Act
    //    inventory.DepositGold(amount);

    //    //Assert
    //    Assert.LessOrEqual(inventory.Gold, inventory.MaxGold);
    //}
    //[Test]
    //[Category("Inventory")]
    //public void InventoryGold_True_StartAtZeroMaxGold()
    //{
    //    //Arrange
    //    Inventory inventory = new Inventory();

    //    //Assert
    //    Assert.AreEqual(0, inventory.Gold);
    //}
    //[Test]
    //[Category("Inventory")]
    //public void DebitGold_True_GoldDebitRemoved()
    //{
    //    //Arrange
    //    Inventory inventory = new Inventory();
    //    int expected = 20;

    //    //Act
    //    inventory.DepositGold(50);
    //    inventory.DebitGold(30);

    //    //Assert
    //    Assert.AreEqual(expected, inventory.Gold);
    //}
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
