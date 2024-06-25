
from pathlib import Path
import time

import numpy as np
from modules import utils
from pipeline import Pipeline


import cv2
import keyboard

class ShuwaDetector(Pipeline):
    
    def __init__(self):
        super().__init__()

  
        self.cap = cv2.VideoCapture(0)
        if not self.cap.isOpened():
            raise Exception("Could not open video device")
        
        # Set video frame width and height if needed
        self.cap.set(cv2.CAP_PROP_FRAME_WIDTH, 640)
        self.cap.set(cv2.CAP_PROP_FRAME_HEIGHT, 480)

        self.translator_manager.load_knn_database()  
    
    def __del__(self):
        if self.cap.isOpened():
            self.cap.release()
    
    def on_record(self):

        try: 
            print("Recording..." if not self.is_recording else "Stop recording")
            self.is_recording = not self.is_recording
            if self.is_recording:
                return


            if len(self.pose_history) < 16:
                print("Video too short.")
                self.reset_pipeline()
                return

            vid_res = {
                "pose_frames": np.stack(self.pose_history),
                "face_frames": np.stack(self.face_history),
                "lh_frames": np.stack(self.lh_history),
                "rh_frames": np.stack(self.rh_history),
                "n_frames": len(self.pose_history)
            }

            feats = self.translator_manager.get_feats(vid_res)
    
            self.reset_pipeline()
            result = self.translator_manager.run_knn(feats)
            return result
        except Exception as e:
            print(f"Error: {e}")
            self.reset_pipeline()
            return None
    
    def main_loop(self):
         
        
        while True:
            ret, frame = self.cap.read()
            if not ret:
                break

                    
            frame = utils.crop_utils.crop_square(frame)
            frame_rgb = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)

            # Apply Holistic Model to the frame            
            self.update(frame_rgb)

            cv2.putText(frame_rgb, "Is Recording" if self.is_recording else "Not Recording",(10, 50), cv2.FONT_HERSHEY_DUPLEX, 1, (203, 52, 247), 1)

            # Display the resulting frame
            cv2.imshow('Frame', frame_rgb)
            
            # Break the loop
            if cv2.waitKey(1) & 0xFF == ord('q'):
                break

        # When everything done, release the capture
        self.cap.release()
        cv2.destroyAllWindows()


if __name__ == "__main__":
    shuwa_detector = ShuwaDetector()
    keyboard.on_press_key("space", lambda e: shuwa_detector.on_record())
    shuwa_detector.main_loop()


