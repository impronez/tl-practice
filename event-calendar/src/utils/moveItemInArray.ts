export const moveItemInArray = <T>(arr: T[], from: number, to: number): T[] => {
  if (from === to) return arr;

  const item = arr[from];
  const withoutItem = [...arr.slice(0, from), ...arr.slice(from + 1)];

  return [...withoutItem.slice(0, to), item, ...withoutItem.slice(to)];
};
