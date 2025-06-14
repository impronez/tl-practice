import type { TaskData, DragState, Action } from "../../types";
import styles from "./TaskCard.module.css";
import { useRef } from "react";
import { useDragAndDrop } from "../../hooks/useDragAndDrop";

interface TaskCardProps {
  data: TaskData;
  columnIndex: number;
  index: number;
  dispatch: React.Dispatch<Action>;
  dragState: DragState;
  moveTask: () => void;
}

export const TaskCard = ({
  data,
  columnIndex,
  index,
  dispatch,
  dragState,
  moveTask,
}: TaskCardProps) => {
  const ref = useRef<HTMLDivElement | null>(null);

  const { isDragging, handleMouseDown, mousePosition } = useDragAndDrop({
    onMouseDown: () => {
      dispatch({
        type: "START_DRAG",
        taskData: data,
        column: columnIndex,
        index,
      });
    },
    onMouseUp: () => {
      moveTask();
    },
    elementRef: ref,
  });

  const isTargetCard =
    dragState.draggingTaskData?.id == data.id &&
    (dragState.sourceColumnId != columnIndex || dragState.sourceIndex != index);

  return (
    <>
      {isDragging && (
        <div
          className={`${styles.cardWrapper} ${styles.draggedCard}`}
          style={{
            top: mousePosition.y,
            left: mousePosition.x,
          }}
        >
          <p className={styles.cardTitle}>{data.title}</p>
        </div>
      )}

      <div
        ref={ref}
        onMouseDown={handleMouseDown}
        className={`${styles.cardWrapper} ${styles.noSelect} ${
          isDragging && styles.originCard
        } ${isTargetCard && styles.targetCard}`}
      >
        <p className={styles.cardTitle} title={data.title}>
          {data.title}
        </p>
      </div>
    </>
  );
};
