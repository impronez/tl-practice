import { Route, Routes, useLocation } from "react-router";
import "./App.scss";
import { Header } from "./components/Header/Header";
import { Home } from "./pages/Home/Home";
import getTitleByPath from "./utils/getTitleFromPath";
import { Dictionary } from "./pages/Dictionary/Dictionary";
import { EditWord } from "./pages/EditWord/EditWord";
import { NewWord } from "./pages/NewWord/NewWord";
import { Check } from "./pages/Check/Check";
import { Result } from "./pages/Result/Result";

function App() {
  const location = useLocation();
  const title = getTitleByPath(location.pathname);

  return (
    <>
      <Header title={title} />
      <div className="app-body">
        <Routes>
          <Route index element={<Home />} />
          <Route path="dictionary" element={<Dictionary />} />
          <Route path="new-word" element={<NewWord />} />
          <Route path="edit-word" element={<EditWord />} />
          <Route path="check" element={<Check />} />
          <Route path="result" element={<Result />} />
        </Routes>
      </div>
    </>
  );
}

export default App;
