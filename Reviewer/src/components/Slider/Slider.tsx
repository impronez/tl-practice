import { useEffect, useRef, useState } from "react";
import styles from "./Slider.module.css";
import AngryFacePath from "../../assets/twemoji_angry-face.svg";
import NeutralFacePath from "../../assets/twemoji_neutral-face.svg";
import GrinningFacePath from "../../assets/twemoji_grinning-face-with-big-eyes.svg";
import FrowningFacePath from "../../assets/twemoji_slightly-frowning-face.svg";
import SmilingFacePath from "../../assets/twemoji_slightly-smiling-face.svg";

interface SliderProps {
  label: string;
  value: number;
  onChange: (value: number) => void;
}

const svgUrls = [
  AngryFacePath,
  FrowningFacePath,
  NeutralFacePath,
  GrinningFacePath,
  SmilingFacePath,
];

const stepColors = ["#f24e1e", "#ff8311", "#ff8311", "#ffc700", "#ffc700"];

export default function Slider({ label, value, onChange }: SliderProps) {
  const scaleSliderRef = useRef<HTMLDivElement>(null);
  const [activeStep, setActiveStep] = useState(0);

  const handleClick = (index: number) => {
    setActiveStep(index);
    onChange?.(index);
  };

  const getScaleBackground = () => {
    if (activeStep === 0) return {};

    const step = activeStep - 1;
    const percentage = (step / 4) * 100;

    return {
      background: `linear-gradient(to right, ${stepColors[step]} ${percentage}%, transparent ${percentage}%)`,
    };
  };

  useEffect(() => {
    setActiveStep(value || 0);
  }, [value]);

  return (
    <div className={styles.sliderContainer}>
      <div
        className={`${styles.sliderScale}`}
        ref={scaleSliderRef}
        style={getScaleBackground()}
      >
        {[...Array(5)].map((_, index) => (
          <div
            key={index}
            className={`${styles.sliderStep} ${activeStep == 0 ? styles[`step-${index + 1}`] : activeStep > index + 1 ? styles[`step-${activeStep}`] : ""}`}
            onMouseDown={() => handleClick(index + 1)}
          >
            {activeStep == index + 1 && (
              <img className={styles.sliderStepActive} src={svgUrls[index]} />
            )}
          </div>
        ))}
      </div>
      <input hidden type="range" min="1" max="5" step="1" />
      <span className={styles.sliderLabel}>{label}</span>
    </div>
  );
}
