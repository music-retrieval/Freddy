#include <iostream>
#include <vector>
#include <essentia/algorithmfactory.h>

extern "C" {
    void essentia_init() {
        essentia::init();
    }

    void essentia_shutdown() {
        essentia::shutdown();
    }

    void essentia_load_audio(const char* audioFilename, double* audioBuffer) {
        std::vector<essentia::Real> vecAudioBuffer;
        essentia::standard::Algorithm* audioLoader = essentia::standard::AlgorithmFactory::create("MonoLoader",
                                                                                                    "filename", audioFilename);
        audioLoader->output("audio").set(vecAudioBuffer);
        audioLoader->compute();

        // Copy data to the provided buffer
        for (size_t i = 0; i < vecAudioBuffer.size(); ++i) {
            audioBuffer[i] = vecAudioBuffer[i];
        }
    }
}
