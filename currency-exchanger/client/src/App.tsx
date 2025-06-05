import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import { CurrencyExchanger } from './components/CurrencyExchanger/CurrencyExchanger';
import { FiltersMenu } from './components/FiltersMenu/FiltersMenu';

function App() {
  return (
    <>
      <FiltersMenu />
      <CurrencyExchanger />
    </>
  );
}

export default App;
