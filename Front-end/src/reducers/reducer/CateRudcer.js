import { GET_CATEGORIES,ADD_CATEGORIES,DELETE_CATEGORIES } from "./../type";
const initialState = {
  categories: [],
};

const cateReducer = (state = initialState, action) => {
  switch (action.type) {
    case GET_CATEGORIES:
      return {
        ...state,
        categories: action.payload,
      };
    case ADD_CATEGORIES:
      return {
				...state,
				categories:  [...state.categories, action.payload]
			}
    case DELETE_CATEGORIES:
      return {
				...state,
				categories: state.categories.filter(item => item.categoryId !== action.payload)
			}
      default:
        return state
  }
};

export default cateReducer;
