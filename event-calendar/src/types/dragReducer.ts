import type { DragState, Action } from "./index.ts";

const initialDragState: DragState = {
  draggingTaskData: null,
  sourceColumnId: null,
  sourceIndex: null,
  targetColumnId: null,
  targetIndex: null,
};

const dragReducer = (state: DragState, action: Action): DragState => {
  switch (action.type) {
    case "START_DRAG":
      return {
        ...state,
        draggingTaskData: action.taskData,
        sourceColumnId: action.column,
        sourceIndex: action.index,
      };
    case "MOVE_TASK":
      return {
        ...state,
        targetColumnId: action.column,
        targetIndex: action.index,
      };
    case "END_DRAG":
      return initialDragState;
  }
};

export { initialDragState, dragReducer };
