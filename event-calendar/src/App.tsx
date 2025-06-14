import "./App.css";
import { useReducer, useState } from "react";
import { Column } from "./components/Column/Column";
import { initialColumnsData } from "./data/initialColumnsData";
import { dragReducer, initialDragState } from "./types/dragReducer";
import type { ColumnData } from "./types";
import { moveItemInArray } from "./utils/moveItemInArray";

function App() {
  const [columns, setColumns] = useState<ColumnData[]>(initialColumnsData);
  const [dragState, dispatch] = useReducer(dragReducer, initialDragState);

  const moveTask = () => {
    const {
      draggingTaskData,
      sourceColumnId,
      sourceIndex,
      targetColumnId,
      targetIndex,
    } = dragState;

    if (
      draggingTaskData != null &&
      sourceColumnId != null &&
      sourceIndex != null &&
      targetColumnId != null &&
      targetIndex != null
    ) {
      setColumns((prevColumns) => {
        const sourceColumn = prevColumns[sourceColumnId];
        const targetColumn = prevColumns[targetColumnId];

        const task = sourceColumn.tasks[sourceIndex];
        if (!task) return prevColumns;

        const newSourceTasks = [
          ...sourceColumn.tasks.slice(0, sourceIndex),
          ...sourceColumn.tasks.slice(sourceIndex + 1),
        ];

        const newTargetTasks =
          sourceColumnId === targetColumnId
            ? moveItemInArray(sourceColumn.tasks, sourceIndex, targetIndex)
            : [
                ...targetColumn.tasks.slice(0, targetIndex),
                task,
                ...targetColumn.tasks.slice(targetIndex),
              ];

        return prevColumns.map((col, i) => {
          if (i === sourceColumnId && i === targetColumnId) {
            return { ...col, tasks: newTargetTasks };
          }

          if (i === sourceColumnId) {
            return { ...col, tasks: newSourceTasks };
          }

          if (i === targetColumnId) {
            return { ...col, tasks: newTargetTasks };
          }

          return col;
        });
      });
    }

    dispatch({ type: "END_DRAG" });
  };

  return (
    <>
      {columns.map((column, index) => (
        <Column
          key={index}
          columnIndex={index}
          data={column}
          dispatch={dispatch}
          dragState={dragState}
          moveTask={moveTask}
        />
      ))}
    </>
  );
}

export default App;
