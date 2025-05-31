type TaskData = {
  id: number;
  title: string;
};

type ColumnData = {
  tasks: (TaskData | null)[];
};

type DragState = {
  draggingTaskData: TaskData | null;
  sourceColumnId: number | null;
  sourceIndex: number | null;
  targetColumnId: number | null;
  targetIndex: number | null;
};

type Action =
  | { type: "START_DRAG"; taskData: TaskData; column: number; index: number }
  | { type: "MOVE_TASK"; column: number; index: number }
  | { type: "END_DRAG" }
  | { type: "RESET" };

export type { TaskData, ColumnData, DragState, Action };
