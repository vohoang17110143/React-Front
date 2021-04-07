import { combineReducers } from 'redux'
import cateReducer from './reducer/CateRudcer';
import brandReduder from './reducer/BrandReducer'
import colorReducer from './reducer/ColorReducer';
import productReducer from './reducer/ProductReducer';


const initialState = {
  sidebarShow: 'responsive'
}

const changeState = (state = initialState, { type, ...rest }) => {
  switch (type) {
    case 'set':
      return {...state, ...rest }
    default:
      return state
  }
}


const rootReducer = combineReducers({
  nav: changeState,
  cate: cateReducer,
  brand:brandReduder,
  color:colorReducer,
  product:productReducer,
});

export default rootReducer
