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
    const base = [...data.tasks];

    if (
      draggingTask &&
      dragState.targetColumnId === columnIndex &&
      hoverIndex !== null
    ) {
      const taskIndex = base.findIndex((t) => t?.id === draggingTask.id);
      if (taskIndex !== -1) {
        base.splice(taskIndex, 1);
      }
      base.splice(hoverIndex, 0, draggingTask);
    }

    return base.length < MAX_TASKS_PER_COLUMN
      ? [...base, ...Array(MAX_TASKS_PER_COLUMN - base.length).fill(null)]
      : base;
  })();

  const handleMouseEnter = (index: number) => {
    if (!draggingTask) return;

    if (index > data.tasks.length) {
      index = data.tasks.length;
    }

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
