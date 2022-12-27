using UnityEngine;
using CraftingAnims;

public class GUIControls : MonoBehaviour
{
	public CrafterController crafterController;

	bool carryItem;
	bool carryToggle;

	public void ResetCarry()
	{
		carryItem = false;
		carryToggle = false;
	}

	void OnGUI()
	{
		if(!crafterController.isMoving && crafterController.isGrounded)
		{
			if(crafterController.charState == CrafterController.CharacterState.Idle)
			{
				if(GUI.Button(new Rect(25, 25, 150, 30), "Get Hammer")) { crafterController.RecieveAction("Get Hammer"); }
				if(GUI.Button(new Rect(195, 25, 150, 30), "Get Paintbrush")) { crafterController.RecieveAction("Get Paintbrush"); }
				if(GUI.Button(new Rect(25, 65, 150, 30), "Get Axe")) { crafterController.RecieveAction("Get Axe"); }
				if(GUI.Button(new Rect(195, 65, 150, 30), "Get Spear")) { crafterController.RecieveAction("Get Spear"); }
				if(GUI.Button(new Rect(25, 105, 150, 30), "Get PickAxe")) { crafterController.RecieveAction("Get PickAxe"); }
				if(GUI.Button(new Rect(25, 145, 150, 30), "Pickup Shovel")) { crafterController.RecieveAction("Pickup Shovel"); }
				if(GUI.Button(new Rect(25, 185, 150, 30), "PullUp Fishing Pole")) { crafterController.RecieveAction("PullUp Fishing Pole"); }
				if(GUI.Button(new Rect(25, 225, 150, 30), "Take Food")) { crafterController.RecieveAction("Take Food"); }
				if(GUI.Button(new Rect(25, 265, 150, 30), "Recieve Drink")){ crafterController.RecieveAction("Recieve Drink"); }
				if(GUI.Button(new Rect(25, 305, 150, 30), "Pickup Box")) { crafterController.RecieveAction("Pickup Box"); }
				if(GUI.Button(new Rect(195, 305, 150, 30), "Pickup Lumber")) { crafterController.RecieveAction("Pickup Lumber"); }
				if(GUI.Button(new Rect(370, 305, 150, 30), "Pickup Overhead")) { crafterController.RecieveAction("Pickup Overhead"); }
				if(GUI.Button(new Rect(25, 345, 150, 30), "Recieve Box")) { crafterController.RecieveAction("Recieve Box"); }
				if(GUI.Button(new Rect(25, 385, 150, 30), "Get Saw")) { crafterController.RecieveAction("Get Saw"); }
				if(GUI.Button(new Rect(25, 425, 150, 30), "Get Sickle")) { crafterController.RecieveAction("Get Sickle"); }
				if(GUI.Button(new Rect(25, 465, 150, 30), "Get Rake")) { crafterController.RecieveAction("Get Rake"); }
				if(GUI.Button(new Rect(200, 465, 150, 30), "Use")) { crafterController.RecieveAction("Use"); }
				if(GUI.Button(new Rect(375, 465, 150, 30), "Crawl")) { crafterController.RecieveAction("Crawl"); }
				if(GUI.Button(new Rect(25, 505, 150, 30), "Sit")) { crafterController.RecieveAction("Sit"); }
				if(GUI.Button(new Rect(200, 505, 150, 30), "Push Cart")) { crafterController.RecieveAction("Push Cart"); }
				if(GUI.Button(new Rect(375, 505, 150, 30), "Laydown")) { crafterController.RecieveAction("Laydown"); }
				if(GUI.Button(new Rect(25, 545, 150, 30), "Gather")) { crafterController.RecieveAction("Gather"); }
				if(GUI.Button(new Rect(200, 545, 150, 30), "Gather Kneeling")) { crafterController.RecieveAction("Gather Kneeling"); }
				if(GUI.Button(new Rect(200, 585, 150, 30), "Wave1")) { crafterController.RecieveAction("Wave1"); }
				if(GUI.Button(new Rect(375, 545, 150, 30), "Cheer1")) { crafterController.RecieveAction("Cheer1"); }
				if(GUI.Button(new Rect(25, 585, 150, 30), "Scratch Head")) { crafterController.RecieveAction("Scratch Head"); }
				if(GUI.Button(new Rect(375, 585, 150, 30), "Cheer2")) { crafterController.RecieveAction("Cheer2"); }
				if(GUI.Button(new Rect(375, 630, 150, 30), "Cheer3")) { crafterController.RecieveAction("Cheer3"); }
				if(GUI.Button(new Rect(375, 670, 150, 30), "Fear")) { crafterController.RecieveAction("Fear"); }
				if(GUI.Button(new Rect(25, 625, 150, 30), "Climb")) { crafterController.RecieveAction("Climb"); }
				if(GUI.Button(new Rect(200, 625, 150, 30), "Climb Top")) { crafterController.RecieveAction("Climb Top"); }
				if(GUI.Button(new Rect(200, 665, 150, 30), "Pray")) { crafterController.RecieveAction("Pray"); }
				if(GUI.Button(new Rect(25, 665, 150, 30), "Push Pull")) { crafterController.RecieveAction("Push Pull"); }
			}
			if(crafterController.charState == CrafterController.CharacterState.Cart)
			{
				if(GUI.Button(new Rect(200, 505, 150, 30), "Release Cart")) { crafterController.RecieveAction("Release Cart"); }
			}
			if(crafterController.charState == CrafterController.CharacterState.Pray)
			{
				if(GUI.Button(new Rect(200, 665, 150, 30), "Stand")) { crafterController.RecieveAction("Stand"); }
			}
			if(crafterController.charState == CrafterController.CharacterState.Hammer)
			{
				if(GUI.Button(new Rect(25, 25, 150, 30), "Hammer Wall")) { crafterController.RecieveAction("Hammer Wall"); }
				if(GUI.Button(new Rect(25, 65, 150, 30), "Hammer Table")) { crafterController.RecieveAction("Hammer Table"); }
				if(GUI.Button(new Rect(25, 105, 150, 30), "Give Hammer")) { crafterController.RecieveAction("Give Item"); }
				if(GUI.Button(new Rect(25, 145, 150, 30), "Put Away Hammer")) { crafterController.RecieveAction("Put Away Item"); }
				if(GUI.Button(new Rect(25, 185, 150, 30), "Put Down Hammer")) { crafterController.RecieveAction("Put Down Item"); }
				if(GUI.Button(new Rect(25, 225, 150, 30), "Drop Hammer")) { crafterController.RecieveAction("Drop Item"); }
				if(GUI.Button(new Rect(25, 265, 150, 30), "Kneel")) { crafterController.RecieveAction("Kneel"); }
				if(GUI.Button(new Rect(25, 305, 150, 30), "Chisel")) { crafterController.RecieveAction("Chisel"); }
			}
			if(crafterController.charState == CrafterController.CharacterState.Painting)
			{
				if(GUI.Button(new Rect(25, 25, 150, 30), "Paint Wall")) { crafterController.RecieveAction("Paint Wall"); }
				if(GUI.Button(new Rect(25, 65, 150, 30), "Fill Brush")) { crafterController.RecieveAction("Fill Brush"); }
				if(GUI.Button(new Rect(25, 105, 150, 30), "Give Paintbrush")) { crafterController.RecieveAction("Give Item"); }
				if(GUI.Button(new Rect(25, 145, 150, 30), "Put Away Paintbrush")) { crafterController.RecieveAction("Put Away Item"); }
				if(GUI.Button(new Rect(25, 185, 150, 30), "Put Down Paintbrush")) { crafterController.RecieveAction("Put Down Item"); }
				if(GUI.Button(new Rect(25, 225, 150, 30), "Drop Paintbrush")) { crafterController.RecieveAction("Drop Item"); }
			}
			if(crafterController.charState == CrafterController.CharacterState.Kneel)
			{
				if(GUI.Button(new Rect(25, 30, 150, 30), "Hammer")) { crafterController.RecieveAction("Hammer"); }
				if(GUI.Button(new Rect(25, 265, 150, 30), "Stand")) { crafterController.RecieveAction("Stand"); }
			}
			if(crafterController.charState == CrafterController.CharacterState.Drink)
			{
				if(GUI.Button(new Rect(25, 25, 150, 30), "Drink")) { crafterController.RecieveAction("Drink"); }
				if(GUI.Button(new Rect(25, 65, 150, 30), "Water")) { crafterController.RecieveAction("Water"); }
				if(GUI.Button(new Rect(25, 105, 150, 30), "Give Drink")) { crafterController.RecieveAction("Give Item"); }
				if(GUI.Button(new Rect(25, 145, 150, 30), "Put Drink Away")) { crafterController.RecieveAction("Put Away Item"); }
				if(GUI.Button(new Rect(25, 185, 150, 30), "Put Drink Down")) { crafterController.RecieveAction("Put Down Item"); }
				if(GUI.Button(new Rect(25, 225, 150, 30), "Drop Drink")) { crafterController.RecieveAction("Drop Item"); }
			}
			if(crafterController.charState == CrafterController.CharacterState.Food)
			{
				if(GUI.Button(new Rect(25, 25, 150, 30), "Eat Food")) { crafterController.RecieveAction("Eat Food"); }
				if(GUI.Button(new Rect(25, 65, 150, 30), "Give Food")) { crafterController.RecieveAction("Give Item"); }
				if(GUI.Button(new Rect(25, 105, 150, 30), "Put Food Down")) { crafterController.RecieveAction("Put Down Item"); }
				if(GUI.Button(new Rect(25, 145, 150, 30), "Put Food Away")) { crafterController.RecieveAction("Put Away Item"); }
				if(GUI.Button(new Rect(25, 185, 150, 30), "Drop Food")) { crafterController.RecieveAction("Drop Item"); }
				if(GUI.Button(new Rect(25, 225, 150, 30), "Plant Food")) { crafterController.RecieveAction("Plant Item"); }
			}
			if(crafterController.charState == CrafterController.CharacterState.Sickle)
			{
				if(GUI.Button(new Rect(25, 25, 150, 30), "Use Sickle")) { crafterController.RecieveAction("Use Sickle"); }
				if(GUI.Button(new Rect(25, 65, 150, 30), "Give Sickle")) { crafterController.RecieveAction("Give Item"); }
				if(GUI.Button(new Rect(25, 105, 150, 30), "Put Sickle Down")) { crafterController.RecieveAction("Put Down Item"); }
				if(GUI.Button(new Rect(25, 145, 150, 30), "Put Sickle Away")) { crafterController.RecieveAction("Put Away Item"); }
				if(GUI.Button(new Rect(25, 185, 150, 30), "Drop Sickle")) { crafterController.RecieveAction("Drop Item"); }
			}
			if(crafterController.charState == CrafterController.CharacterState.Axe)
			{
				if(GUI.Button(new Rect(25, 25, 150, 30), "Start Chopping")) { crafterController.RecieveAction("Start Chopping"); }
				if(GUI.Button(new Rect(25, 65, 150, 30), "Put Axe Away")) { crafterController.RecieveAction("Put Away Item"); }
				if(GUI.Button(new Rect(25, 105, 150, 30), "Give Axe")) { crafterController.RecieveAction("Give Item"); }
				if(GUI.Button(new Rect(25, 145, 150, 30), "Put Axe Down")) { crafterController.RecieveAction("Put Down Item"); }
				if(GUI.Button(new Rect(25, 185, 150, 30), "Drop Axe")) { crafterController.RecieveAction("Drop Item"); }
			}
			if(crafterController.charState == CrafterController.CharacterState.PickAxe)
			{
				if(GUI.Button(new Rect(25, 25, 150, 30), "Start PickAxing")) { crafterController.RecieveAction("Start PickAxing"); }
				if(GUI.Button(new Rect(25, 65, 150, 30), "Put PickAxe Away")) { crafterController.RecieveAction("Put Away Item"); }
				if(GUI.Button(new Rect(25, 105, 150, 30), "Give PickAxe")) { crafterController.RecieveAction("Give Item"); }
				if(GUI.Button(new Rect(25, 145, 150, 30), "Put PickAxe Down")) { crafterController.RecieveAction("Put Down Item"); }
				if(GUI.Button(new Rect(25, 185, 150, 30), "Drop PickAxe")) { crafterController.RecieveAction("Drop Item"); }
			}
			if(crafterController.charState == CrafterController.CharacterState.Saw)
			{
				if(GUI.Button(new Rect(25, 25, 150, 30), "Start Sawing")) { crafterController.RecieveAction("Start Sawing"); }
				if(GUI.Button(new Rect(25, 65, 150, 30), "Put Saw Away")) { crafterController.RecieveAction("Put Away Item"); }
				if(GUI.Button(new Rect(25, 105, 150, 30), "Give Saw")) { crafterController.RecieveAction("Give Item"); }
				if(GUI.Button(new Rect(25, 145, 150, 30), "Drop Saw")) { crafterController.RecieveAction("Drop Item"); }
			}
			if(crafterController.charState == CrafterController.CharacterState.Sawing)
			{
				if(GUI.Button(new Rect(25, 25, 150, 30), "Finish Sawing")) { crafterController.RecieveAction("Finish Sawing"); }
			}
			if(crafterController.charState == CrafterController.CharacterState.Chopping)
			{
				if(GUI.Button(new Rect(25, 25, 150, 30), "Chop Vertical")) { crafterController.RecieveAction("Chop Vertical"); }
				if(GUI.Button(new Rect(25, 65, 150, 30), "Chop Horizontal")) { crafterController.RecieveAction("Chop Horizontal"); }
				if(GUI.Button(new Rect(25, 105, 150, 30), "Chop Diagonal")) { crafterController.RecieveAction("Chop Diagonal"); }
				if(GUI.Button(new Rect(25, 145, 150, 30), "Chop Ground")) { crafterController.RecieveAction("Chop Ground"); }
				if(GUI.Button(new Rect(25, 185, 150, 30), "Finish Chopping")) { crafterController.RecieveAction("Finish Chopping"); }
			}
			if(crafterController.charState == CrafterController.CharacterState.PickAxing)
			{
				if(GUI.Button(new Rect(25, 25, 150, 30), "Swing Vertical")) { crafterController.RecieveAction("Swing Vertical"); }
				if(GUI.Button(new Rect(25, 65, 150, 30), "Swing Horizontal")) { crafterController.RecieveAction("Swing Horizontal"); }
				if(GUI.Button(new Rect(25, 105, 150, 30), "Swing Ground")) { crafterController.RecieveAction("Swing Ground"); }
				if(GUI.Button(new Rect(25, 145, 150, 30), "Swing Ceiling")) { crafterController.RecieveAction("Swing Ceiling"); }
				if(GUI.Button(new Rect(25, 185, 150, 30), "Swing Diagonal")) { crafterController.RecieveAction("Swing Diagonal"); }
				if(GUI.Button(new Rect(25, 225, 150, 30), "Finish PickAxing")) { crafterController.RecieveAction("Finish PickAxing"); }
			}
			if(crafterController.charState == CrafterController.CharacterState.Shovel)
			{
				if(GUI.Button(new Rect(25, 25, 150, 30), "Start Digging")) { crafterController.RecieveAction("Start Digging"); }
				if(GUI.Button(new Rect(25, 65, 150, 30), "Put Shovel Away")) { crafterController.RecieveAction("Put Away Item"); }
				if(GUI.Button(new Rect(25, 105, 150, 30), "Give Shovel")) { crafterController.RecieveAction("Give Item"); }
				if(GUI.Button(new Rect(25, 145, 150, 30), "Put Shovel Down")) { crafterController.RecieveAction("Put Down Item"); }
				if(GUI.Button(new Rect(25, 185, 150, 30), "Drop Shovel")) { crafterController.RecieveAction("Drop Item"); }
			}
			if(crafterController.charState == CrafterController.CharacterState.Rake)
			{
				if(GUI.Button(new Rect(25, 25, 150, 30), "Start Raking")) { crafterController.RecieveAction("Start Raking"); }
				if(GUI.Button(new Rect(25, 65, 150, 30), "Put Rake Away")) { crafterController.RecieveAction("Put Away Item"); }
				if(GUI.Button(new Rect(25, 105, 150, 30), "Give Rake")) { crafterController.RecieveAction("Give Item"); }
				if(GUI.Button(new Rect(25, 145, 150, 30), "Put Rake Down")) { crafterController.RecieveAction("Put Down Item"); }
				if(GUI.Button(new Rect(25, 185, 150, 30), "Drop Rake")) { crafterController.RecieveAction("Drop Item"); }
			}
			if(crafterController.charState == CrafterController.CharacterState.Raking)
			{
				if(GUI.Button(new Rect(25, 25, 150, 30), "Rake")) { crafterController.RecieveAction("Rake"); }
				if(GUI.Button(new Rect(25, 65, 150, 30), "Finish Raking")) { crafterController.RecieveAction("Finish Raking"); }
			}
			if(crafterController.charState == CrafterController.CharacterState.Digging)
			{
				if(GUI.Button(new Rect(25, 25, 150, 30), "Dig")) { crafterController.RecieveAction("Dig"); }
				if(GUI.Button(new Rect(25, 65, 150, 30), "Finish Digging")) { crafterController.RecieveAction("Finish Digging"); }
			}
			if(crafterController.charState == CrafterController.CharacterState.FishingPole)
			{
				if(GUI.Button(new Rect(25, 25, 150, 30), "Cast Reel")) { crafterController.RecieveAction("Cast Reel"); }
				if(GUI.Button(new Rect(25, 65, 150, 30), "Put Fishing Pole Away")) { crafterController.RecieveAction("Put Away Item"); }
				if(GUI.Button(new Rect(25, 105, 150, 30), "Give Fishing Pole")) { crafterController.RecieveAction("Give Item"); }
				if(GUI.Button(new Rect(25, 145, 150, 30), "Put Fishing Pole Down")) { crafterController.RecieveAction("Put Down Item"); }
				if(GUI.Button(new Rect(25, 185, 150, 30), "Drop FishingPole")) { crafterController.RecieveAction("Drop Item"); }
			}
			if(crafterController.charState == CrafterController.CharacterState.Sit)
			{
				if(GUI.Button(new Rect(25, 25, 150, 30), "Talk1")) { crafterController.RecieveAction("Talk1"); }
				if(GUI.Button(new Rect(25, 65, 150, 30), "Eat")) { crafterController.RecieveAction("Eat"); }
				if(GUI.Button(new Rect(25, 105, 150, 30), "Drink")) { crafterController.RecieveAction("Drink"); }
				if(GUI.Button(new Rect(25, 145, 150, 30), "Stand")) { crafterController.RecieveAction("Stand"); }
			}
			if(crafterController.charState == CrafterController.CharacterState.Fishing)
			{
				if(GUI.Button(new Rect(25, 25, 150, 30), "Reel In")) { crafterController.RecieveAction("Reel In"); }
				if(GUI.Button(new Rect(25, 65, 150, 30), "Finish Fishing")) { crafterController.RecieveAction("Finish Fishing"); }
			}
			if(crafterController.charState == CrafterController.CharacterState.Box)
			{
				if(GUI.Button(new Rect(25, 25, 150, 30), "Put Down Box")) { crafterController.RecieveAction("Put Down Box"); }
				if(GUI.Button(new Rect(25, 65, 150, 30), "Throw Box")) { crafterController.RecieveAction("Throw Box"); }
				if(GUI.Button(new Rect(25, 104, 150, 30), "Give Box")) { crafterController.RecieveAction("Give Box"); }
			}
			if(crafterController.charState == CrafterController.CharacterState.Lumber)
			{
				if(GUI.Button(new Rect(25, 25, 150, 30), "Put Down Lumber")) { crafterController.RecieveAction("Put Down Lumber"); }
			}
			if(crafterController.charState == CrafterController.CharacterState.Overhead)
			{
				if(GUI.Button(new Rect(25, 25, 150, 30), "Throw Sphere")) { crafterController.RecieveAction("Throw Sphere"); }
			}
			if(crafterController.charState == CrafterController.CharacterState.Climb)
			{
				if(GUI.Button(new Rect(25, 25, 150, 30), "Climb Off Bottom")) { crafterController.RecieveAction("Climb Off Bottom"); }
				if(GUI.Button(new Rect(25, 65, 150, 30), "Climb Up")) { crafterController.RecieveAction("Climb Up"); }
				if(GUI.Button(new Rect(25, 105, 150, 30), "Climb Down")) { crafterController.RecieveAction("Climb Down"); }
				if(GUI.Button(new Rect(25, 145, 150, 30), "Climb Off Top")) { crafterController.RecieveAction("Climb Off Top"); }
			}
			if(crafterController.charState == CrafterController.CharacterState.PushPull)
			{
				if(GUI.Button(new Rect(25, 25, 150, 30), "Release")) { crafterController.RecieveAction("Release"); }
			}
			if(crafterController.charState == CrafterController.CharacterState.Laydown)
			{
				if(GUI.Button(new Rect(375, 505, 150, 30), "Getup")) { crafterController.RecieveAction("Getup"); }
			}
			if(crafterController.charState == CrafterController.CharacterState.Use)
			{
				if(GUI.Button(new Rect(200, 465, 150, 30), "Stop Use")) { crafterController.RecieveAction("Stop Use"); }
			}
			if(crafterController.charState == CrafterController.CharacterState.Crawl)
			{
				if(GUI.Button(new Rect(375, 465, 150, 30), "Getup")) { crafterController.RecieveAction("Getup"); }
			}
			if(crafterController.charState == CrafterController.CharacterState.Spear)
			{
				if(GUI.Button(new Rect(25, 65, 150, 30), "Start Spearfishing")) { crafterController.RecieveAction("Start Spearfishing"); }
				if(GUI.Button(new Rect(25, 105, 150, 30), "Give Spear")) { crafterController.RecieveAction("Give Item"); }
				if(GUI.Button(new Rect(25, 145, 150, 30), "Put Away Spear")) { crafterController.RecieveAction("Put Away Item"); }
				if(GUI.Button(new Rect(25, 185, 150, 30), "Put Down Spear")) { crafterController.RecieveAction("Put Down Item"); }
				if(GUI.Button(new Rect(25, 225, 150, 30), "Drop Spear")) { crafterController.RecieveAction("Drop Item"); }
			}
			if(crafterController.charState == CrafterController.CharacterState.Spearfishing)
			{
				if(GUI.Button(new Rect(25, 25, 150, 30), "Spear")) { crafterController.RecieveAction("Spear"); }
				if(GUI.Button(new Rect(25, 65, 150, 30), "Finish Spearfishing")) { crafterController.RecieveAction("Finish Spearfishing"); }
			}

			//Carry Item animation override.
			if(crafterController.charState == CrafterController.CharacterState.Hammer
				|| crafterController.charState == CrafterController.CharacterState.Painting
				|| crafterController.charState == CrafterController.CharacterState.Drink
				|| crafterController.charState == CrafterController.CharacterState.Food
				|| crafterController.charState == CrafterController.CharacterState.Sickle
				|| crafterController.charState == CrafterController.CharacterState.Axe
				|| crafterController.charState == CrafterController.CharacterState.PickAxe
				|| crafterController.charState == CrafterController.CharacterState.Shovel
				|| crafterController.charState == CrafterController.CharacterState.Rake)
			{
				carryItem = GUI.Toggle(new Rect(500, 15, 100, 30), carryItem, "Carry Item");
				if(carryItem)
				{
					if(!carryToggle)
					{
						carryToggle = true;
						crafterController.CarryItem(true);
					}
				}
				else if(!carryItem)
				{
					if(carryToggle)
					{
						carryToggle = false;
						crafterController.CarryItem(false);
					}
				}
			}
		}
	}
}