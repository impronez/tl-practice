export const moveItemInArray = <T>(arr: T[], from: number, to: number): T[] => {
  if (from === to) return arr;

  const copy = [...arr];
  const value = copy.splice(from, 1)[0];

  copy.splice(to, 0, value);

  return copy;
};
