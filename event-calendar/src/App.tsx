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
        const updated = [...prevColumns];

        const sourceColumn = { ...updated[sourceColumnId] };
        const sourceTasks = [...sourceColumn.tasks];

        const task = sourceTasks[sourceIndex];
        if (!task) return prevColumns;

        sourceTasks.splice(sourceIndex, 1);

        if (sourceColumnId === targetColumnId) {
          const tasks = moveItemInArray(
            sourceColumn.tasks,
            sourceIndex,
            targetIndex
          );

          updated[sourceColumnId] = {
            ...sourceColumn,
            tasks: tasks,
          };
        } else {
          const targetColumn = { ...updated[targetColumnId] };
          const targetTasks = [...targetColumn.tasks];

          targetTasks.splice(targetIndex, 0, task);

          updated[sourceColumnId] = {
            ...sourceColumn,
            tasks: sourceTasks,
          };

          updated[targetColumnId] = {
            ...targetColumn,
            tasks: targetTasks,
          };
        }

        return updated;
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
