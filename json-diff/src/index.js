import { Navigation } from "./navigation.js";
import { initJsonDiff } from "./form-controller.js";
import { initAuth } from "./auth-controller.js";

Navigation.init();
initAuth();
initJsonDiff();
