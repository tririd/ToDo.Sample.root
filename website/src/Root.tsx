import { Route, Routes, useNavigate } from 'react-router-dom';
import Home from './pages/Home';
import NotFound from './pages/NotFound';
import ToDo from './pages/ToDo';

export default function Root() {
    const navigate = useNavigate();
    return (
      <div>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/todo" element={<ToDo />} />
          <Route path="*" element={<NotFound />} />
        </Routes>
      </div>
    );
  }