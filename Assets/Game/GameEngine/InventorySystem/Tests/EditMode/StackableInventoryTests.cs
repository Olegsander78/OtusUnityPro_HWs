using NUnit.Framework;

namespace Game.GameEngine.InventorySystem
{
    public sealed class StackableInventoryTests
    {
        [Test]
        public void WhenInventoryIsEmpty_ByDefault()
        {
            //Arrange:
            var inventory = new StackableInventory();

            //Assert:
            Assert.True(inventory.IsEmpty());
        }

        [Test]
        public void WhenAddItem_ThenInventoryShouldBeNotEmplty()
        {
            //Arrange:
            const string itemName = "sample";
            var inventory = new StackableInventory();
            var item = new InventoryItem(itemName, InventoryItemFlags.NONE, metadata: null);

            //Act:
            var isItemAdded = inventory.AddItemAsInstance(item);

            //Assert:
            Assert.True(isItemAdded);
            Assert.False(inventory.IsEmpty());
            Assert.True(inventory.IsItemExists(item));
        }

        [Test]
        public void WhenAdd2StackableItems_ThenInventorySlotsShouldBe2()
        {
            //Arrange:
            const string itemName = "sample";
            var inventory = new StackableInventory();
            
            var item1 = new InventoryItem(
                itemName,
                InventoryItemFlags.STACKABLE,
                metadata: null,
                new Component_Stackable(size: 5, value: 3)
            );
            
            var item2 = new InventoryItem(
                itemName,
                InventoryItemFlags.STACKABLE,
                metadata: null,
                new Component_Stackable(size: 5, value: 3)
            );
            
            //Act:
            inventory.AddItemAsInstance(item1);
            inventory.AddItemAsInstance(item2);
            
            //Assert:
            Assert.AreEqual(2, inventory.GetAllItems().Length);
            Assert.AreEqual(6, inventory.CountAllItems());
        }

        [Test]
        public void WhenSpawnItem_ThenInventoryShouldBeNotEmplty()
        {
            //Arrange:
            var inventory = new StackableInventory();
            var itemPrototype = new InventoryItem("sample", InventoryItemFlags.NONE, metadata: null);

            //Act:
            inventory.AddItemsByPrototype(itemPrototype, 1);

            //Assert:
            Assert.False(inventory.IsEmpty());
        }

        [Test]
        public void WhenSpawn1Item_Then1ItemShouldBeFound()
        {
            //Arrange:
            const string itemName = "sample";
            var inventory = new StackableInventory();
            var itemPrototype = new InventoryItem(itemName, InventoryItemFlags.NONE, metadata: null);

            //Act:
            inventory.AddItemsByPrototype(itemPrototype, 1);

            //Assert:
            var isItemFound = inventory.FindItemFirst(itemName, out var inventoryItem);
            Assert.True(isItemFound);
            Assert.True(inventory.IsItemExists(inventoryItem));
            Assert.True(inventory.CountAllItems() == 1);
        }

        [Test]
        public void WhenSpawn0Items_ThenItemShouldBeNotFound()
        {
            //Arrange:
            const string itemName = "sample";
            var inventory = new StackableInventory();
            var itemPrototype = new InventoryItem(itemName, InventoryItemFlags.NONE, metadata: null);

            //Act:
            inventory.AddItemsByPrototype(itemPrototype, 0);

            //Assert:
            var isItemFound = inventory.FindItemFirst(itemName, out var inventoryItem);
            Assert.False(isItemFound);
            Assert.False(inventory.IsItemExists(inventoryItem));
            Assert.True(inventory.CountAllItems() == 0);
        }

        [Test]
        public void WhenSpawnItem_ThenItemShouldBeDifferentFromPrototype()
        {
            //Arrange:
            const string itemName = "sample";
            var inventory = new StackableInventory();
            var itemPrototype = new InventoryItem(itemName, InventoryItemFlags.NONE, metadata: null);

            //Act:
            inventory.AddItemsByPrototype(itemPrototype, 1);

            //Assert:
            inventory.FindItemFirst(itemName, out var inventoryItem);
            Assert.AreNotEqual(itemPrototype, inventoryItem);
        }

        [Test]
        public void WhenSpawn5SimpleItems_Then5SimpleItemsShouldBeFound()
        {
            //Arrange:
            const string itemName = "sample";
            var inventory = new StackableInventory();
            var itemPrototype = new InventoryItem(itemName, InventoryItemFlags.NONE, metadata: null);

            //Act:
            inventory.AddItemsByPrototype(itemPrototype, 5);

            //Assert:
            var inventoryItems = inventory.FindItems(itemName);
            Assert.True(inventoryItems.Length == 5);
            Assert.True(inventory.CountItems(itemName) == 5);
            Assert.True(inventory.CountAllItems() == 5);
        }

        [Test]
        public void WhenSpawn5StackableItems_Then1ContainerShouldBeFound()
        {
            //Arrange:
            const string itemName = "sample";
            var inventory = new StackableInventory();
            var itemPrototype = new InventoryItem(
                itemName,
                InventoryItemFlags.STACKABLE,
                metadata: null,
                new Component_Stackable(size: 5)
            );

            //Act:
            inventory.AddItemsByPrototype(itemPrototype, 5);

            //Assert:
            var inventoryItems = inventory.FindItems(itemName);
            Assert.True(inventoryItems.Length == 1);
            Assert.True(inventory.CountItems(itemName) == 5);
            Assert.True(inventory.CountAllItems() == 5);

            var item = inventoryItems[0];
            var stackableComponent = item.GetComponent<IComponent_Stackable>();
            Assert.True(stackableComponent.Size == 5);
            Assert.True(stackableComponent.Value == 5);
            Assert.True(stackableComponent.IsFull);
        }

        [Test]
        public void WhenSpawn12StackableItems_Then3ContainersShouldBeFound()
        {
            //Arrange:
            const string itemName = "sample";
            var inventory = new StackableInventory();
            var itemPrototype = new InventoryItem(
                itemName,
                InventoryItemFlags.STACKABLE,
                metadata: null,
                new Component_Stackable(size: 5)
            );

            //Act:
            inventory.AddItemsByPrototype(itemPrototype, 12);

            //Assert:
            var inventoryItems = inventory.FindItems(itemName);
            Assert.AreEqual(12, inventory.CountItems(itemName));
            Assert.AreEqual(12, inventory.CountAllItems());
            Assert.AreEqual(3, inventoryItems.Length);

            var item1 = inventoryItems[0];
            var component1 = item1.GetComponent<IComponent_Stackable>();
            Assert.True(component1.Size == 5);
            Assert.True(component1.Value == 5);
            Assert.True(component1.IsFull);

            var item2 = inventoryItems[1];
            var component2 = item2.GetComponent<IComponent_Stackable>();
            Assert.True(component2.Size == 5);
            Assert.True(component2.Value == 5);
            Assert.True(component2.IsFull);

            var item3 = inventoryItems[2];
            var component3 = item3.GetComponent<IComponent_Stackable>();
            Assert.True(component3.Size == 5);
            Assert.True(component3.Value == 2);
            Assert.False(component3.IsFull);
        }

        [Test]
        public void WhenSpawn9StackableItems_ThenShouldBe2Containers()
        {
            //Arrange:
            const string itemName = "sample";
            var inventory = new StackableInventory();
            var itemPrototype1 = new InventoryItem(
                itemName,
                InventoryItemFlags.STACKABLE,
                metadata: null,
                new Component_Stackable(size: 5)
            );

            //Act:
            inventory.AddItemsByPrototype(itemPrototype1, 3);
            inventory.AddItemsByPrototype(itemPrototype1, 6);

            //Assert:
            var inventoryItems = inventory.FindItems(itemName);
            Assert.True(inventoryItems.Length == 2);
            Assert.True(inventory.CountItems(itemName) == 9);
            Assert.True(inventory.CountAllItems() == 9);

            var item1 = inventoryItems[0];
            var component1 = item1.GetComponent<IComponent_Stackable>();
            Assert.AreEqual(5, component1.Size);
            Assert.AreEqual(5, component1.Value);
            Assert.True(component1.IsFull);

            var item2 = inventoryItems[1];
            var component2 = item2.GetComponent<IComponent_Stackable>();
            Assert.AreEqual(5, component2.Size);
            Assert.AreEqual(4, component2.Value);
            Assert.False(component2.IsFull);
        }

        [Test]
        public void WhenRemoveAbsentSimpleItem_ThenRemovingShouldBeFalse()
        {
            //Arrange:
            const string itemName = "sample";
            var inventory = new StackableInventory();
            var inventoryItem = new InventoryItem(itemName, InventoryItemFlags.NONE, metadata: null);

            //Act:
            var isItemRemoved = inventory.RemoveItem(inventoryItem.Name);

            //Assert:
            Assert.False(isItemRemoved);
        }

        [Test]
        public void WhenRemoveSimpleItem_ThenItemShouldBeAbsentInInventory()
        {
            //Arrange:
            const string itemName = "sample";
            var inventory = new StackableInventory();
            var prototype = new InventoryItem(itemName, InventoryItemFlags.NONE, metadata: null);
            
            inventory.AddItemsByPrototype(prototype, 1);
            inventory.FindItemFirst(itemName, out var item);
            var countBefore = inventory.CountAllItems();

            //Act:
            var isItemRemoved = inventory.RemoveItem(item.Name);
            var countAfter = inventory.CountItems(itemName);

            //Assert:
            Assert.True(isItemRemoved);
            Assert.AreEqual(1, countBefore);
            Assert.AreEqual(0, countAfter);
            Assert.True(inventory.IsEmpty());
        }

        [Test]
        public void WhenRemoveAbsentStackableItem_ThenRemovingShouldBeFalse()
        {
            //Arrange:
            const string itemName = "sample";
            var inventory = new StackableInventory();
            var inventoryItem = new InventoryItem(
                itemName,
                InventoryItemFlags.STACKABLE,
                metadata: null,
                new Component_Stackable(size: 5)
            );

            //Act:
            var isItemRemoved = inventory.RemoveItem(inventoryItem.Name);

            //Assert:
            Assert.False(isItemRemoved);
        }

        [Test]
        public void WhenRemove1StackableItem_Then4StackableItemsShouldBeLeft()
        {
            //Arrange:
            const string itemName = "sample";
            var inventory = new StackableInventory();
            var prototype = new InventoryItem(
                itemName,
                InventoryItemFlags.STACKABLE,
                metadata: null,
                new Component_Stackable(size: 5)
            );
            inventory.AddItemsByPrototype(prototype, 5);
            inventory.FindItemFirst(itemName, out var item);
            
            var countBeforeInInventory = inventory.CountItems(itemName);
            var countBeforeInStack = item.GetComponent<IComponent_Stackable>().Value;

            //Act:
            var isItemRemoved = inventory.RemoveItem(item.Name);

            //Assert:
            Assert.True(isItemRemoved);
            Assert.AreEqual(5, countBeforeInInventory);
            Assert.AreEqual(5, countBeforeInStack);

            var countAfterInInventory = inventory.CountItems(itemName);
            var countAfterInStack = item.GetComponent<IComponent_Stackable>().Value;
            Assert.AreEqual(4, countAfterInInventory);
            Assert.AreEqual(4, countAfterInStack);
            Assert.True(inventory.IsItemExists(item));
            Assert.AreEqual(1, inventory.FindItems(itemName).Length);
        }

        [Test]
        public void WhenRemove5StackableItem_Then3StackableItemsShouldBeLeft()
        {
            //Arrange:
            const string itemName = "sample";
            var inventory = new StackableInventory();
            var prototype = new InventoryItem(
                itemName,
                InventoryItemFlags.STACKABLE,
                metadata: null,
                new Component_Stackable(size: 5)
            );
            
            inventory.AddItemsByPrototype(prototype, 8);
            var items = inventory.FindItems(itemName);
            var item1 = items[0];
            var item2 = items[1];
            
            var countBeforeInInventory = inventory.CountItems(itemName);
            var lengthBeforeInInventory = items.Length;

            //Act:
            inventory.RemoveItems(itemName, 5);

            //Assert:
            Assert.AreEqual(8, countBeforeInInventory);
            Assert.AreEqual(2, lengthBeforeInInventory);

            var countAfterInInventory = inventory.CountItems(itemName);
            Assert.AreEqual(3, countAfterInInventory);

            var countAfterInStack1 = item1.GetComponent<IComponent_Stackable>().Value;
            Assert.AreEqual(3, countAfterInStack1);

            var countAfterInStack2 = item2.GetComponent<IComponent_Stackable>().Value;
            Assert.AreEqual(0, countAfterInStack2);

            Assert.True(inventory.IsItemExists(item1));
            Assert.False(inventory.IsItemExists(item2));

            var lengthAfterInInventory = inventory.FindItems(itemName).Length;
            Assert.AreEqual(1, lengthAfterInInventory);
        }
    }
}