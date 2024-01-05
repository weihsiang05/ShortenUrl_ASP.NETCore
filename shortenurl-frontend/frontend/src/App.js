import { BrowserRouter, Routes, Route } from 'react-router-dom'
import HomePage from './pages/homePage';
import RedirectUrl from './pages/redirectUrl';
import Navbar from './components/Navbar';

function App() {

  // Function to conditionally render Navbar based on route
  const renderNavbar = () => {
    const currentRoute = window.location.pathname;

    // Render Navbar only if the current route is "/"
    if (currentRoute === '/') {
      return <Navbar />;
    }

    return null;
  };

  return (
    <div className="App">
      <BrowserRouter>
        {/* <Navbar /> */}
        {renderNavbar()}
        <div className='pages'>
          <Routes>
            <Route
              path="/"
              element={<HomePage />}
            />
            <Route
              path="/:randomLetter"
              element={<RedirectUrl />}
            />
          </Routes>
        </div>
      </BrowserRouter>
    </div>
  );
}

export default App;
