import { initNavigation } from "./navigation.js";
import { initAuth } from "./auth-controller.js";
import { initJsonValidator } from "./form-controller.js";

initAuth();
initNavigation();
initJsonValidator();
