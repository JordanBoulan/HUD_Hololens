using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// The Compass Script
/// This script controls central compass behaviour and CompassMarker management \n
///
/// Place TheCompass prefab in your scene, and attach CompassCamera prefab as a child \n
/// of your player or equivalent game avatar. Compass Camera should have the same \n
/// position and rotation as your main camera. For example, make it a child of the \n
/// Standard "First Person Controller" Game Object. The CompassCamera is used to \n 
/// determine position relative to marked objects. Any offset will affect the \n
/// accuracy of distance/position calculations. \n
/// Copyright 2016 While Fun Games \n
/// http://whilefun.com
/// </summary>

public class TheCompassScript : MonoBehaviour {

    // You can replace these textures with your own graphics in the Inspector
    /// <value> The main Compass GUI graphic. </value>
    public GameObject myCanvas;
    /// <value> The compass background GUI graphic. </value>
	public Sprite compassGUIBackground;
	/// <value> Turn Bookend graphic On and Off. </value>
	public bool drawCompassBookends = true;
	public Sprite compassGUIBookEndLeft;
	public Sprite compassGUIBookEndRight;
	/// <value> Turn Ticks graphic On and Off. </value>
	public bool drawCompassTicks = true;
	public Sprite compassGUITicks;
    /// <value> Compass North Label. </value>
	public Sprite compassGUILabelNorth;
    /// <value> Compass South Label. </value>
	public Sprite compassGUILabelSouth;
    /// <value> Compass East Label. </value>
	public Sprite compassGUILabelEast;
    /// <value> Compass West Label. </value>
	public Sprite compassGUILabelWest;
    /// <value> Turn Up/Down arrow graphic On and Off. </value>
    public bool drawUpDownArrows = true;
    /// <value> Turn Up/Down Arrow graphic padding On and Off. When Off, vertical position of Distance text is static. </value>
    public bool padUpDownArrows = true;
    /// <value> The vertical pixel offset of up/down arrows. </value>
    public float upDownArrowOffset = 0.0f;
	public Sprite compassMarkerUpArrow;
	public Sprite compassMarkerDownArrow;

    // Distance Text
    /// <value> Turn Distance Text On and Off </value>
    public bool drawDistanceText = true;
    /// <value> The vertical pixel offset of the distance text. </value>
    public float distanceTextOffset = 0.0f;
	public Color compassGUITextColor = Color.white;
    /// <value> Turn Distance Text Outline On and Off </value>
    public bool outlineDistanceText = true;
	public Color compassGUITextOutlineColor = Color.black;
    /// <value> Default font size (Scales with screen size changes) </value>
    public int compassBaseFontSize = 12;
    // How far up/down a marker needs to be in order to be flagged with up/down arrow on the compass
    /// <value> Min vertical distance before drawing up/down arrows above Compass Marker </value>
    public float verticalMarkerDistanceThreshold = 2.0f;
    // The min/max distance are the bounds in which we draw the distance to marker over top of the marker
    /// <value> Min distance to draw distance indicator above Compass Marker </value>
    public float minMarkerDistance = 2.0f;
	/// <value> Max distance to draw distance indicator above Compass Marker </value>
	public float maxMarkerDistance = 50.0f;
	/// <value> Arrow icons to point toward a Marker when hints are enabled on it </value>
	public Sprite offScreenArrowLeft;
	public Sprite offScreenArrowRight;

    // Internal, GUI, and UI canvas stuff
    private float currentDirectionInDegrees = 0.0f;
	private GameObject worldPoles;
	private float deltaFromWorldPoles = 0.0f;
	private float directionInDegrees = 0.0f;

	// GUI stuff
	private float compassGUINorthPosition = 0.0f;
	private float compassGUISouthPosition = 0.0f;
	private float compassGUIEastPosition = 0.0f;
	private float compassGUIWestPosition = 0.0f;
	private float distanceBetweenDirectionsOnCompass = 0.0f;

	// UI Canvas Stuff
	private RectTransform compassBackground;
	private RectTransform compassBackgroundMask;
	private RectTransform compassTicksRect;
	private Vector2 tempTicksPos = Vector2.zero;
	private RectTransform northLabel;
	private RectTransform southLabel;
	private RectTransform eastLabel;
	private RectTransform westLabel;
	private RectTransform compassMarkerContainer;
	private RectTransform compassCanvasParent;
	private RectTransform compassBookendLeft;
	private RectTransform compassBookendRight;
	private GameObject compassCamera;
	private Vector2 compassBackgroundAnchoredPosition = Vector2.zero;

    /// <summary>
	/// To add more icon types for Compass Markers:
	/// 1) Create the graphic using provided template
	/// 2) Add a new IconType enum value for the new icon type (e.g. eMyIconType)
	/// 3) Add a new public Sprite below (e.g. markerIconMyNewType)
	/// 4) Add case for new enum value in getSpriteFromIconType() function below
    /// </summary>
	public enum IconType {eGeneric, eImportant, eMonster, eCustom};
	public Sprite markerIconGeneric;
	public Sprite markerIconImportant;
	public Sprite markerIconMonster;

    /// <summary>
    /// Gets the required Unity objects (world poles, and camera). 
    /// </summary>
	public void Awake(){
		
		worldPoles = GameObject.FindGameObjectWithTag("CompassWorldPoles");
		if(!worldPoles){
			Debug.LogError("Cannot find CompassWorldPoles object. Is the WorldPoles tag missing or prefab missing?");
			#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
			#endif
		}

		RectTransform[] rtc = gameObject.GetComponentsInChildren<RectTransform>();
		foreach(RectTransform rt in rtc){

			// This is where the compass markers live. It is z-fixed behind the 
			// bookends so that markers always go behind them
			if(rt.gameObject.name == "CompassMarkerContainer"){
				compassMarkerContainer = rt;
			}else if(rt.gameObject.name == "CompassCanvas"){
				compassCanvasParent = rt;
			}else if(rt.gameObject.name == "CompassBackground"){
				compassBackground = rt;
			}else if(rt.gameObject.name == "CompassBackgroundMask"){
				compassBackgroundMask = rt;
			}else if(rt.gameObject.name == "CompassTicks"){
				compassTicksRect = rt;
			}else if(rt.gameObject.name == "NorthLabel"){
				northLabel = rt;
			}else if(rt.gameObject.name == "SouthLabel"){
				southLabel = rt;
			}else if(rt.gameObject.name == "EastLabel"){
				eastLabel = rt;
			}else if(rt.gameObject.name == "WestLabel"){
				westLabel = rt;
			}else if(rt.gameObject.name == "BookendLeft"){
				compassBookendLeft = rt;
			}else if(rt.gameObject.name == "BookendRight"){
				compassBookendRight = rt;
			}
			
		}

		// If this error occurs, you probably broke the prefab, as it's missing some required elements
		if(!compassMarkerContainer || !compassCanvasParent || !compassBackground || !compassBackgroundMask || !compassTicksRect || !northLabel || !southLabel || !eastLabel || !westLabel || !compassBookendLeft || !compassBookendRight){
          // Debug.LogError("TheCompassScript:: Could not find one or more of the child UI Canvas elements of The Compass. Did you break the Prefab?");
        }

		// Compass Camera will be used to calculate where our compass is.
		// We don't make The Compass direct child of player because it can cause subtle 
		// texture movements as camera and canvas will move/adjust at the same time.
		compassCamera = GameObject.FindGameObjectWithTag("CompassCamera");
		if(!compassCamera){
			Debug.LogError("TheCompassScript:: Could not find Compass Camera!");
		}
		
	}

    /// <summary>
    /// Grabs the different graphics in Unity, and assigns them to variables, and save our position on start.
    /// </summary>
	public void Start(){
        myCanvas = GameObject.Find("CompassCanvas");
        //myCanvas.transform.Translate(new Vector3(0,-300,0), Space.Self);
        // Assign all our specified graphics - i.e. "Apply the Compass Theme"
		compassBackground.GetComponent<Image>().overrideSprite = compassGUIBackground;
		compassBackgroundMask.GetComponent<Image>().overrideSprite = compassGUIBackground;
		northLabel.GetComponent<Image>().overrideSprite = compassGUILabelNorth;
		southLabel.GetComponent<Image>().overrideSprite = compassGUILabelSouth;
		eastLabel.GetComponent<Image>().overrideSprite = compassGUILabelEast;
		westLabel.GetComponent<Image>().overrideSprite = compassGUILabelWest;

		// Note: If your implementation will always exclude Ticks graphic, you can delete the ticks completely rather than just hiding them
		// Same with Bookends. If the theme/skin you want to use does not require bookends, you can delete them completely as well.
		if(drawCompassTicks){
			compassTicksRect.GetComponent<Image>().overrideSprite = compassGUITicks;
		}else{
			compassTicksRect.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
		}
		if(drawCompassBookends){
			compassBookendLeft.GetComponent<Image>().overrideSprite = compassGUIBookEndLeft;
			compassBookendRight.GetComponent<Image>().overrideSprite = compassGUIBookEndRight;
		}else{
			//compassBookendLeft.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
			//dompassBookendRight.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
		}

		// Save our position on start, as this affects where markers/arrows/etc. are placed on the canvas
		compassBackgroundAnchoredPosition.x = compassBackground.anchoredPosition.x;
		compassBackgroundAnchoredPosition.y = compassBackground.anchoredPosition.y;

	}

    /// <summary>
    /// This function adjusts the the the graphics (ticks and labels) on the UI depending on which way the 
    /// camera/user is facing. It also ensures that if the user is not facing a certain direction, that 
    /// label will not be displayed on the UI.
    /// </summary>
    public void Update(){

		// Determine our direction relative to WorldPoles
		currentDirectionInDegrees = compassCamera.transform.rotation.eulerAngles.y;
		deltaFromWorldPoles = currentDirectionInDegrees - worldPoles.GetComponent<Transform>().rotation.eulerAngles.y;

		if(deltaFromWorldPoles < 0){
			directionInDegrees = (360+deltaFromWorldPoles);
		}else if(deltaFromWorldPoles > 0){
			directionInDegrees = deltaFromWorldPoles;
		}else{
			directionInDegrees = 0.0f;
		}

		distanceBetweenDirectionsOnCompass = 1.5f*compassBackground.rect.width;

		if(directionInDegrees >= 180){
			compassGUINorthPosition = compassBackground.anchoredPosition.x + ((360-directionInDegrees)/180)*distanceBetweenDirectionsOnCompass;
			compassGUISouthPosition = compassBackground.anchoredPosition.x + ((180-directionInDegrees)/180)*distanceBetweenDirectionsOnCompass;
			compassGUIEastPosition = compassBackground.anchoredPosition.x + ((90-directionInDegrees)/180)*distanceBetweenDirectionsOnCompass;
			compassGUIWestPosition = compassBackground.anchoredPosition.x + ((270-directionInDegrees)/180)*distanceBetweenDirectionsOnCompass;
		}else{
			compassGUINorthPosition = compassBackground.anchoredPosition.x - (directionInDegrees/180)*distanceBetweenDirectionsOnCompass;
			compassGUISouthPosition = compassBackground.anchoredPosition.x - ((directionInDegrees-180)/180)*distanceBetweenDirectionsOnCompass;
			compassGUIEastPosition = compassBackground.anchoredPosition.x - ((directionInDegrees-90)/180)*distanceBetweenDirectionsOnCompass;
			compassGUIWestPosition = compassBackground.anchoredPosition.x - ((directionInDegrees-270)/180)*distanceBetweenDirectionsOnCompass;
		}
		
		// Update direction label positions
		Vector2 tempLabelPosition = Vector2.zero;

		// Only show if in bounds, otherwise just make it invisible

        // North label
		if(compassGUINorthPosition > (compassBackground.anchoredPosition.x - compassBackground.rect.width/2 + northLabel.rect.width/2) && compassGUINorthPosition < (compassBackground.anchoredPosition.x + compassBackground.rect.width/2 - northLabel.rect.width/2)){
			northLabel.GetComponent<CanvasRenderer>().SetAlpha(1.0f);
			tempLabelPosition = northLabel.anchoredPosition;
			tempLabelPosition.x = compassGUINorthPosition;
			northLabel.anchoredPosition = tempLabelPosition;
		}else{
			northLabel.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
		}

        // South label
		if(compassGUISouthPosition > (compassBackground.anchoredPosition.x - compassBackground.rect.width/2 + southLabel.rect.width/2) && compassGUISouthPosition < (compassBackground.anchoredPosition.x + compassBackground.rect.width/2 - southLabel.rect.width/2)){
			southLabel.GetComponent<CanvasRenderer>().SetAlpha(1.0f);
			tempLabelPosition = southLabel.anchoredPosition;
			tempLabelPosition.x = compassGUISouthPosition;
			southLabel.anchoredPosition = tempLabelPosition;
		}else{
			southLabel.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
		}
		
        // East label
		if(compassGUIEastPosition > (compassBackground.anchoredPosition.x - compassBackground.rect.width/2 + eastLabel.rect.width/2) && compassGUIEastPosition < (compassBackground.anchoredPosition.x + compassBackground.rect.width/2 - eastLabel.rect.width/2)){
			eastLabel.GetComponent<CanvasRenderer>().SetAlpha(1.0f);
			tempLabelPosition = eastLabel.anchoredPosition;
			tempLabelPosition.x = compassGUIEastPosition;
			eastLabel.anchoredPosition = tempLabelPosition;
		}else{
			eastLabel.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
		}
		
        // West label
		if(compassGUIWestPosition > (compassBackground.anchoredPosition.x - compassBackground.rect.width/2 + westLabel.rect.width/2) && compassGUIWestPosition < (compassBackground.anchoredPosition.x + compassBackground.rect.size.x/2 - westLabel.rect.width/2)){
			westLabel.GetComponent<CanvasRenderer>().SetAlpha(1.0f);
			tempLabelPosition = westLabel.anchoredPosition;
			tempLabelPosition.x = compassGUIWestPosition;
			westLabel.anchoredPosition = tempLabelPosition;
		}else{
			westLabel.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
		}

		// Move Ticks based on North position, and  mod by 256 so that we don't scroll to far (BG images are 512px wide)
		tempTicksPos.x = (compassGUINorthPosition % 256);
		compassTicksRect.anchoredPosition = tempTicksPos;

	}

    /// <summary>
    /// Adds a marker icon to compass
    /// Specify an icon type if desired, otherwise it is assumed you are using a custom icon
    /// Note: This function is different than the others as it places the UI element as a child of 
    /// the CompassMarkerContrainer, which is not offset from the main canvas at all. 
    /// </summary>
    /// <param name="iconToAdd"> Add the icon the the UI </param>
    /// <param name="iconType"> The icon to be added to the UI </param>
    public void putIconOnCompass(RectTransform iconToAdd, IconType iconType=IconType.eCustom){

		iconToAdd.SetParent(compassMarkerContainer, false);
		Vector2 tempMarkerIconPosition = Vector2.zero;
		tempMarkerIconPosition.y = 0.0f;
		iconToAdd.anchoredPosition = tempMarkerIconPosition;

		// If we're not using a custom icon, then apply the correct icon to this UI element
		if(iconType != IconType.eCustom){
			iconToAdd.GetComponent<Image>().overrideSprite = getSpriteFromIconType(iconType);
		}

	}

    /// <summary>
    /// Adds distance label to compass, and applies all formatting options specified.
    /// </summary>
    public void putDistanceLabelOnCompass(RectTransform distanceLabelToAdd){

		distanceLabelToAdd.SetParent(compassMarkerContainer, false);
		Vector2 tempDistanceLabelPosition = Vector2.zero;
		tempDistanceLabelPosition.y = distanceTextOffset;
		distanceLabelToAdd.anchoredPosition = tempDistanceLabelPosition;

		// Update font size, color, and outline color (or remove outline completely)
		distanceLabelToAdd.GetComponent<Text>().color = compassGUITextColor;
		distanceLabelToAdd.GetComponent<Text>().fontSize = compassBaseFontSize;

		if(outlineDistanceText && distanceLabelToAdd.GetComponent<Outline>()){
			distanceLabelToAdd.GetComponent<Outline>().effectColor = compassGUITextOutlineColor;
		}else{
			//This assumes you will not try to dynamically add/remove outline effects at runtime
			Destroy(distanceLabelToAdd.GetComponent<Outline>());
		}

	}

    /// <summary>
    /// Add up/down arrows to indicate vertical position difference
    /// </summary>
    public void putUpDownArrowOnCompass(RectTransform upDownArrowToAdd){
		
		upDownArrowToAdd.SetParent(compassMarkerContainer, false);
		Vector2 tempDistanceLabelPosition = Vector2.zero;
		tempDistanceLabelPosition.y = upDownArrowOffset;
		upDownArrowToAdd.anchoredPosition = tempDistanceLabelPosition;
	}

    /// <summary>
    /// Add off screen hints to compass. Note these are designed for single item use only. They become unreadable
	/// very quickly if more than one set of hints is visible off the same side of the screen at the same time.
    /// </summary>
    /// <param name="offScreenHintLeft"> </param>
    /// <param name="iconType"> </param>
	public void putOffscreenHintOnCompass(RectTransform offScreenHintLeft, IconType iconType=IconType.eCustom){

		offScreenHintLeft.SetParent(compassCanvasParent, false);

		// If we're not using a custom icon, then apply the correct icon to this UI element
		if(iconType != IconType.eCustom){
			offScreenHintLeft.GetComponent<CompassMarkerOffScreenHintScript>().updateIconSprite(getSpriteFromIconType(iconType));
		}

	}

    /// <summary>
    /// The function ensures that the correct image shows up on the compass. (eg. looking north will display the north texture).
    /// </summary>
    /// <param name="iconType"> The icon to be displayed on the UI. </param>
    /// <returns> Returns correct Sprite for iconType specified. </returns>
	public Sprite getSpriteFromIconType(IconType iconType){
		
		Sprite spriteForIconType = markerIconGeneric;
		
		switch(iconType){
			
		case IconType.eGeneric:
			spriteForIconType = markerIconGeneric;
			break;
			
		case IconType.eImportant:
			spriteForIconType = markerIconImportant;
			break;
			
		case IconType.eMonster:
			spriteForIconType = markerIconMonster;
			break;
			
		/*
		// Your new Marker Icon case here:
		case IconType.eMyIconType:
			spriteForIconType = markerIconMyNewType;
			break;
		*/
			
		// Default to the generic icon
		default:
			spriteForIconType = markerIconGeneric;
			break;
			
		}
		
		return spriteForIconType;
		
	}
    /// <summary>
    /// Calculate and returns the updated Compass GUI position.
    /// </summary>
    /// <param name="markerTransform"> Transform the textures on the UI. </param>
    /// <param name="widthOfUIElement"> The width of the element shown on the UI. </param>
    /// <returns> 
    /// Returns Mathf.INFINITY if marker is out of bounds RIGHT of the GUI (and should not be drawn)
    /// Returns Mathf.NEGATIVEINFINITY if marker is out of bounds LEFT of the GUI (and should not be drawn)
    /// </returns>
    public float getUpdatedGUIPositionForMarker(Transform markerTransform, float widthOfUIElement){

		Vector3 relative = compassCamera.transform.InverseTransformPoint(markerTransform.position);
		float tempAngle = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;
		float newGUIPosition = 0.0f;
		newGUIPosition = ((compassBackground.anchoredPosition.x) + (tempAngle/180)*distanceBetweenDirectionsOnCompass);

		// If out of bounds, set to infinity +/-
		if(newGUIPosition > (compassBackground.anchoredPosition.x + compassBackground.rect.width/2 - widthOfUIElement/2)){
			newGUIPosition = Mathf.Infinity;
		}else if(newGUIPosition < (compassBackground.anchoredPosition.x - compassBackground.rect.width/2 + widthOfUIElement/2)){
			newGUIPosition = Mathf.NegativeInfinity;
		}

		return newGUIPosition;

	}
    /// <summary>
    /// Calculates and returns vertical positional difference between specified marker transform and compass transform
    /// Used for determining which, if any, up/down arrow icons to display.
    /// </summary>
    /// <param name="markerTransform"> Transform the textures on the UI. </param>
    /// <returns> Returns up or down arrow Sprite, or null if no sprite should be displayed. </returns>
    public Sprite getUpdatedUpDownArrowSprite(Transform markerTransform){

		Sprite updatedSprite = null;

		if(markerTransform.position.y > (compassCamera.transform.position.y + verticalMarkerDistanceThreshold)){
			updatedSprite = compassMarkerUpArrow;
		}else if(markerTransform.position.y < (compassCamera.transform.position.y - verticalMarkerDistanceThreshold)){
			updatedSprite = compassMarkerDownArrow;
		}

		return updatedSprite;

	}

    /// <summary>
    /// Calculates and returns distance between compass and specified transform. If the text is 
    /// is outside the range specified, it will be hidden from view.
    /// </summary>
    /// <param name="markerTransform"></param>
    /// <returns> Returns Mathf.INFINITY if the distance is outside the bounds specidfied </returns>
    public float getUpdatedDistanceToCompass(Transform markerTransform){

		float compassDistance = Vector3.Distance(compassCamera.transform.position, markerTransform.position);

		if(compassDistance < minMarkerDistance || compassDistance > maxMarkerDistance){
			compassDistance = Mathf.Infinity;
		}

		return compassDistance;

	}

}
