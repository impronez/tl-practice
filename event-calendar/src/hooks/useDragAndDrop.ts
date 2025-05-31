import { useEffect, useRef, useState } from "react";

interface Point {
  x: number;
  y: number;
}

interface UseDragAndDropOptions {
  onMouseDown?: (e: React.MouseEvent) => void;
  onMouseUp?: () => void;
  elementRef: React.RefObject<HTMLElement | null>;
  elementSize?: { width: number; height: number };
}
export const useDragAndDrop = ({
  onMouseDown,
  onMouseUp,
  elementRef,
  elementSize = { width: 200, height: 200 },
}: UseDragAndDropOptions) => {
  const [isDragging, setIsDragging] = useState(false);
  const [mousePosition, setMousePosition] = useState<Point>({ x: 0, y: 0 });
  const offsetRef = useRef<Point>({ x: 0, y: 0 });

  const handleMouseDown = (e: React.MouseEvent) => {
    if (!elementRef.current) return;

    const rect = elementRef.current.getBoundingClientRect();
    offsetRef.current = {
      x: e.clientX - rect.left,
      y: e.clientY - rect.top,
    };

    setMousePosition({
      x: e.clientX - offsetRef.current.x,
      y: e.clientY - offsetRef.current.y,
    });

    setIsDragging(true);
    onMouseDown?.(e);
  };

  useEffect(() => {
    if (!isDragging) return;

    const handleMouseUp = () => {
      onMouseUp?.();
      setIsDragging(false);
    };

    const handleMouseMove = (e: MouseEvent) => {
      const rawLeft = e.clientX - offsetRef.current.x;
      const rawTop = e.clientY - offsetRef.current.y;

      const clampedLeft = Math.max(
        0,
        Math.min(rawLeft, window.innerWidth - elementSize.width)
      );

      const clampedTop = Math.max(
        0,
        Math.min(rawTop, window.innerHeight - elementSize.height)
      );

      setMousePosition({ x: clampedLeft, y: clampedTop });
    };

    const handleWindowBlur = () => {
      handleMouseUp();
    };

    window.addEventListener("mousemove", handleMouseMove);
    window.addEventListener("mouseup", handleMouseUp);
    window.addEventListener("blur", handleWindowBlur);

    return () => {
      window.removeEventListener("mousemove", handleMouseMove);
      window.removeEventListener("mouseup", handleMouseUp);
      window.removeEventListener("blur", handleWindowBlur);
    };
  }, [isDragging, elementSize, onMouseUp]);

  return {
    isDragging,
    mousePosition,
    handleMouseDown,
  };
};
