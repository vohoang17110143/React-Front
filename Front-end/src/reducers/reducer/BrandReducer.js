import { GET_BRANDS,ADD_BRANDS,DELETE_BRANDS,SEARCH_BRANDS } from "./../type";
const initialState = {
  brands: [],
};

const brandReduder = (state = initialState, action) => {
  switch (action.type) {
    case GET_BRANDS:
      return {
        ...state,
        brands: action.payload,
      };
    case ADD_BRANDS:
      return {
				...state,
				brands:  [...state.brands, action.payload]
			}
    case DELETE_BRANDS:
      return {
				...state,
				brands: state.brands.filter(item => item.brandId !== action.payload)
			}
    case SEARCH_BRANDS:
      return{
        ...state,
        brands: state.brands.filter(item =>  item.name.includes(action.payload))
      }
      default:
        return state
  }
};

export default brandReduder;
