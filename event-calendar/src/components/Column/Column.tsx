import { useState } from "react";
import type { ColumnData, DragState, Action } from "../../types";
import { TaskCard } from "../TaskCard/TaskCard";
import styles from "./Column.module.css";

interface ColumnProps {
  data: ColumnData;
  columnIndex: number;
  dispatch: React.Dispatch<Action>;
  dragState: DragState;
  moveTask: () => void;
}

const MAX_TASKS_PER_COLUMN = 5;

export const Column = ({
  data,
  columnIndex,
  dispatch,
  dragState,
  moveTask,
}: ColumnProps) => {
  const [hoverIndex, setHoverIndex] = useState<number | null>(null);

  const draggingTask = dragState.draggingTaskData;

  const displayedTasks = (() => {
    const tasks = data.tasks;

    const isDraggingOverThisColumn =
      draggingTask &&
      dragState.targetColumnId === columnIndex &&
      hoverIndex !== null;

    const updatedTasks = isDraggingOverThisColumn
      ? (() => {
          const withoutDragging = tasks.filter(
            (t) => t?.id !== draggingTask.id
          );
          return [
            ...withoutDragging.slice(0, hoverIndex),
            draggingTask,
            ...withoutDragging.slice(hoverIndex),
          ];
        })()
      : tasks;

    const resultTasks = [
      ...updatedTasks,
      ...Array(Math.max(0, MAX_TASKS_PER_COLUMN - updatedTasks.length)).fill(
        null
      ),
    ];

    return resultTasks.slice(0, MAX_TASKS_PER_COLUMN);
  })();

  const handleMouseEnter = (inputIndex: number) => {
    if (!draggingTask) return;

    const index = Math.min(inputIndex, data.tasks.length);

    if (
      dragState.targetColumnId !== columnIndex ||
      dragState.targetIndex !== index
    ) {
      dispatch({ type: "MOVE_TASK", column: columnIndex, index });
      setHoverIndex(index);
    }
  };

  const handleMouseLeave = () => {
    setHoverIndex(null);
  };

  return (
    <div className={styles.columnWrapper} onMouseLeave={handleMouseLeave}>
      {displayedTasks.map((task, index) => (
        <div
          key={task ? task.id : `placeholder-${index}`}
          onMouseEnter={() => handleMouseEnter(index)}
        >
          {task && (
            <TaskCard
              data={task}
              columnIndex={columnIndex}
              index={index}
              dispatch={dispatch}
              dragState={dragState}
              moveTask={moveTask}
            />
          )}
        </div>
      ))}
    </div>
  );
};
