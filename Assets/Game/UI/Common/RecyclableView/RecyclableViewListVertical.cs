using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public sealed class RecyclableViewListVertical : RecyclableViewList
{
    private static readonly Vector2 zeroVector = Vector2.zero;

    private Vector2 previousAnchoredPos;

    private RectTransform viewport, content;

    private IAdapter adapter;

    [Space]
    [Tooltip("The recyclable pool must cover (viewPort * _poolCoverage) area")]
    [SerializeField]
    private float minPoolCoverage = 1.5f;

    [Tooltip("View pool must have a min size")]
    [SerializeField]
    private int minPoolSize = 10;

    [Tooltip("Threshold for recycling above and below viewport")]
    [SerializeField]
    private float recyclingThreshold = .2f;

    [Space]
    [SerializeField]
    private float spacing;

    //View dimensions
    private float _viewWidth, _viewHeight;

    //Pool Generation
    private List<RectTransform> _cellPool;
    private Bounds _recyclableViewBounds = new();

    //Temps, Flags 
    private readonly Vector3[] _corners = new Vector3[4];
    private bool _recycling;

    //Trackers
    private int currentItemCount; //item count corresponding to the datasource.
    private int topMostCellIndex, bottomMostCellIndex; //Topmost and bottommost cell in the heirarchy

    public override void Initialize(IAdapter adapter)
    {
        this.adapter = adapter;
        this.viewport = this.scrollRect.viewport;
        this.content = this.scrollRect.content;

        this.content.SetTopAnchor();
        this.content.anchoredPosition = Vector3.zero;

        this.StartCoroutine(this.InitializeRoutine());
    }

    public override void Terminate()
    {
        this.scrollRect.onValueChanged.RemoveListener(this.OnPositionChanged);
        this.DestroyViews();
    }

    private IEnumerator InitializeRoutine()
    {
        //Using coroutine for init because few UI stuff requires a frame to update
        yield return null;
        this.SetRecyclingBounds();

        //Cell Poool
        CreateViews();
        currentItemCount = _cellPool.Count;
        topMostCellIndex = 0;
        bottomMostCellIndex = _cellPool.Count - 1;


        //Set content height according to no of rows
        int noOfRows = (int)Mathf.Ceil(_cellPool.Count);
        float contentYSize = noOfRows * (_viewHeight + spacing);
        this.content.sizeDelta = new Vector2(this.content.sizeDelta.x, contentYSize);
        this.content.SetTopAnchor();

        this.scrollRect.onValueChanged.AddListener(this.OnPositionChanged);
    }

    private void CreateViews()
    {
        //Reseting Pool
        this.DestroyViews();
        _cellPool = new List<RectTransform>();

        //Set the prototype cell active and set cell anchor as top 
        viewPrefab.gameObject.SetActive(true);
        var prototypeTransform = viewPrefab.GetComponent<RectTransform>();
        prototypeTransform.SetTopAnchor();

        //Temps
        float currentPoolCoverage = 0;
        int poolSize = 0;
        float posY = 0;

        //set new cell size according to its aspect ratio
        _viewWidth = this.content.rect.width;
        _viewHeight = prototypeTransform.sizeDelta.y / prototypeTransform.sizeDelta.x * _viewWidth;

        //Get the required pool coverage and mininum size for the Cell pool
        float requriedCoverage = minPoolCoverage * this.viewport.rect.height;
        int minPoolSize = Math.Min(this.minPoolSize, this.adapter.GetDataCount());

        //create cells untill the Pool area is covered and pool size is the minimum required
        while ((poolSize < minPoolSize || currentPoolCoverage < requriedCoverage) &&
               poolSize < this.adapter.GetDataCount())
        {
            //Instantiate and add to Pool
            RectTransform item = Instantiate(viewPrefab.gameObject).GetComponent<RectTransform>();
            item.name = "View";
            item.sizeDelta = new Vector2(_viewWidth, _viewHeight);
            _cellPool.Add(item);
            item.SetParent(this.content, false);

            item.anchoredPosition = new Vector2(0, posY);
            posY = item.anchoredPosition.y - (item.rect.height + this.spacing);
            currentPoolCoverage += item.rect.height;

            //Setting data for Cell
            this.adapter.OnCreateView(view: item, index: poolSize);

            //Update the Pool size
            poolSize++;
        }
    }

    private void DestroyViews()
    {
        if (_cellPool == null)
        {
            return;
        }

        foreach (var view in _cellPool)
        {
            this.adapter.OnDestroyView(view);
            Destroy(view.gameObject);
        }

        _cellPool.Clear();
    }

    private void OnPositionChanged(Vector2 normalizedPos)
    {
        var direction = this.scrollRect.content.anchoredPosition - this.previousAnchoredPos;
        this.scrollRect.MoveContentStartPosition(this.DoScroll(direction));
        this.previousAnchoredPos = this.scrollRect.content.anchoredPosition;
    }

    private Vector2 DoScroll(Vector2 direction)
    {
        if (_recycling || _cellPool == null || _cellPool.Count == 0)
        {
            return zeroVector;
        }

        //Updating Recyclable view bounds since it can change with resolution changes.
        SetRecyclingBounds();

        if (direction.y > 0 && _cellPool[bottomMostCellIndex].MaxY() > _recyclableViewBounds.min.y)
        {
            return RecycleTopToBottom();
        }

        if (direction.y < 0 && _cellPool[topMostCellIndex].MinY() < _recyclableViewBounds.max.y)
        {
            return RecycleBottomToTop();
        }

        return zeroVector;
    }

    private void SetRecyclingBounds()
    {
        this.viewport.GetWorldCorners(_corners);
        float threshHold = recyclingThreshold * (_corners[2].y - _corners[0].y);
        _recyclableViewBounds.min = new Vector3(_corners[0].x, _corners[0].y - threshHold);
        _recyclableViewBounds.max = new Vector3(_corners[2].x, _corners[2].y + threshHold);
    }

    /// <summary>
    /// Recycles cells from top to bottom in the List heirarchy
    /// </summary>
    private Vector2 RecycleTopToBottom()
    {
        _recycling = true;

        int n = 0;

        //to determine if content size needs to be updated
        //Recycle until cell at Top is avaiable and current item count smaller than datasource
        while (_cellPool[topMostCellIndex].MinY() > _recyclableViewBounds.max.y &&
               currentItemCount < this.adapter.GetDataCount())
        {
            //Move top cell to bottom
            var posY = _cellPool[bottomMostCellIndex].anchoredPosition.y -
                       _cellPool[bottomMostCellIndex].sizeDelta.y -
                       spacing;
            _cellPool[topMostCellIndex].anchoredPosition =
                new Vector2(_cellPool[topMostCellIndex].anchoredPosition.x, posY);

            //Cell for row at
            this.adapter.OnUpdateView(
                view: _cellPool[topMostCellIndex],
                index: currentItemCount
            );

            //set new indices
            bottomMostCellIndex = topMostCellIndex;
            topMostCellIndex = (topMostCellIndex + 1) % _cellPool.Count;

            currentItemCount++;
            n++;
        }

        //Content anchor position adjustment.
        _cellPool.ForEach(cell =>
            cell.anchoredPosition += n * Vector2.up * (_cellPool[topMostCellIndex].sizeDelta.y + spacing));
        this.content.anchoredPosition -= n * Vector2.up * (_cellPool[topMostCellIndex].sizeDelta.y + spacing);
        _recycling = false;
        return -new Vector2(0, n * (_cellPool[topMostCellIndex].sizeDelta.y + spacing));
    }

    /// <summary>
    /// Recycles cells from bottom to top in the List heirarchy
    /// </summary>
    private Vector2 RecycleBottomToTop()
    {
        _recycling = true;

        int n = 0;

        //to determine if content size needs to be updated
        //Recycle until cell at bottom is avaiable and current item count is greater than cellpool size
        while (_cellPool[bottomMostCellIndex].MaxY() < _recyclableViewBounds.min.y &&
               currentItemCount > _cellPool.Count)
        {
            //Move bottom cell to top
            var posY = _cellPool[topMostCellIndex].anchoredPosition.y + _cellPool[topMostCellIndex].sizeDelta.y +
                       spacing;
            _cellPool[bottomMostCellIndex].anchoredPosition =
                new Vector2(_cellPool[bottomMostCellIndex].anchoredPosition.x, posY);
            n++;

            currentItemCount--;

            //Cell for row at
            this.adapter.OnUpdateView(
                view: _cellPool[bottomMostCellIndex],
                index: currentItemCount - _cellPool.Count
            );

            //set new indices
            topMostCellIndex = bottomMostCellIndex;
            bottomMostCellIndex = (bottomMostCellIndex - 1 + _cellPool.Count) % _cellPool.Count;
        }

        _cellPool.ForEach(cell =>
            cell.anchoredPosition -= n * Vector2.up * (_cellPool[topMostCellIndex].sizeDelta.y + spacing));
        this.content.anchoredPosition += n * Vector2.up * (_cellPool[topMostCellIndex].sizeDelta.y + spacing);
        _recycling = false;
        return new Vector2(0, n * (_cellPool[topMostCellIndex].sizeDelta.y + spacing));
    }
}